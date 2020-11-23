using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using DiscUtils.Iso9660;
using DiscUtils.BootConfig;
using DiscUtils.Common;
using DiscUtils.Partitions;
using DiscUtils.Vfs;
using DiscUtils.Vdi;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;



namespace MH2_BINEX
{

    using GdiColor = System.Drawing.Color;

    public struct Afs_Header_Obj
    {
        public Int32 offset;
        public Int32 size;
    }

    public partial class FRM_MAIN : Form
    {

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        static void MinimizeFootPrint()
        {
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }


        public static Image_Data.Image_Data_Obj Img;
        public static Image_Data.PrimaryVolume_Obj PrimaryVolObj;
        public Image_Data.File_Obj[] Root_Files = new Image_Data.File_Obj[0];

        public int relative_off;


        public Afs_Header_Obj[] MAIN_HEADER = new Afs_Header_Obj[0];
        public Afs_Header_Obj[] DATA_AFS_HEADER = new Afs_Header_Obj[0];
        public Afs_Header_Obj[] UNKAFS_HEADER = new Afs_Header_Obj[0];
        public OpenFileDialog OFD = new OpenFileDialog();

        public AFS_IO AFSIO = new AFS_IO();

       
        
        public FRM_MAIN()
        {
            InitializeComponent();
            
        }
        
        private void AFS_LV_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                PopUp_Menu.Show(Cursor.Position);
            }
        }

        private void extractSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vol_index = Img.Volume_Index; // u need to pass this
            int total_selected = 0;
            List<int> selected_offsets = new List<int>();
            List<int> selected_Sizes = new List<int>();
            List<string> Selected_Files = new List<string>();


            for (int i = 0; i < AFS_LV.SelectedItems.Count; i++)
            {
                selected_offsets.Add(int.Parse(AFS_LV.Items[AFS_LV.SelectedIndices[i]].SubItems[1].Text));
                selected_Sizes.Add(int.Parse(AFS_LV.Items[AFS_LV.SelectedIndices[i]].SubItems[2].Text));
                Selected_Files.Add(AFS_LV.Items[AFS_LV.SelectedIndices[i]].SubItems[3].Text);

                Debug_Log.AppendText("\n\nIndex[" + i.ToString() + "]" + " Sel Off: " + selected_offsets[i].ToString() + " Sel File:  " + Selected_Files[i].ToString());

            }


            total_selected = Selected_Files.Count;


            if (File.Exists(Img.Image_Path))
            {
                using (FileStream fs = new FileStream(Img.Image_Path, FileMode.Open))
                {
                    if (Valid_Iso(fs))
                    {

                        Img.Read_Image = new CDReader(fs, true, true);
                        Img.Root_FSys_Info = Img.Read_Image.Root.GetFileSystemInfos();
                        Stream memStream = Img.Read_Image.OpenFile(Img.Selected_Volume, FileMode.Open);
                        BinaryReader br = new BinaryReader(memStream);

                        int sel_off = int.Parse(AFS_LV.Items[AFS_LV.SelectedIndices[0]].SubItems[1].Text);
                        int sel_sz = int.Parse(AFS_LV.Items[AFS_LV.SelectedIndices[0]].SubItems[2].Text);
                        relative_off = int.Parse(lbl_relativeOffset.Text);


                        // if total selected files is 1, export single file, else run multi export routine..
                        if (total_selected != 0 && total_selected == 1)
                        {

                            if (Img.Selected_Volume.ToUpper() == "DATA.BIN")
                            {
                                AFSIO.Export_File(memStream, AFS_LV, sel_off, relative_off, sel_sz, true, lbl_stats);
                            } else
                            {
                                AFSIO.Export_File(memStream, AFS_LV, sel_off, relative_off, sel_sz, false, lbl_stats);
                            }
                           
                        }
                        else
                        {

                            if (Img.Selected_Volume.ToUpper() == "DATA.BIN")
                            {
                                AFSIO.Export_SelectedFIles(memStream, AFS_LV, total_selected, sel_off, relative_off, selected_offsets, selected_Sizes, Selected_Files, true, ProgressBar00, lbl_stats);
                            }
                            else
                            {
                                AFSIO.Export_SelectedFIles(memStream, AFS_LV, total_selected, sel_off, relative_off, selected_offsets, selected_Sizes, Selected_Files, false, ProgressBar00, lbl_stats);
                            }

                        }




                        br.Close();
                        fs.Close();

                    }
                }
                    }

                    } // selected extract

        

        public bool Valid_Iso(Stream fs)
        {
            if (CDReader.Detect(fs))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Invalid ISO!");
                return false;
            }
        }

        public void Read_Image()
        {
            OpenFileDialog OFD = new OpenFileDialog();

            Stream sstream;

            List<string> Vol_List = new List<string>();

            OFD.Filter = ".AFS()|*.afs|.iso(MHDOS)|*.iso|All Files (*.*)|*.*"; ;
            OFD.FilterIndex = 3;
            OFD.ShowDialog();


            try
            {
                if (File.Exists(OFD.FileName))
                {
                    using (FileStream fs = new FileStream(OFD.FileName, FileMode.Open))
                    {

                        if (Valid_Iso(fs))
                        {
                            Img.Read_Image = new CDReader(fs, true, true);
                            BinaryReader br = new BinaryReader(fs);
                            Img.VolumeManager = new DiscUtils.VolumeManager();
                            Img.Image_Path = OFD.FileName;

                            int Dir_Count = 0;
                            int File_Count = 0;

                            Img.VolumeManager.AddDisk(fs);

                           

                            Array.Resize(ref Img.Logical_VolumeInfo, Img.VolumeManager.GetLogicalVolumes().Length);
                            Array.Resize(ref Img.Physical_VolumeInfo, Img.VolumeManager.GetPhysicalVolumes().Length);

                            Img.Logical_VolumeInfo = Img.VolumeManager.GetLogicalVolumes();
                            Img.Physical_VolumeInfo = Img.VolumeManager.GetPhysicalVolumes();

                            Img.RootDirectory_Info = Img.Read_Image.Root.GetDirectories();
                            Img.Root_FInfo = Img.Read_Image.Root.GetFiles();
                            Img.Root_FSystem_Opts = Img.Read_Image.Root.FileSystem.Options;
                            Img.Root_FSys_Info = Img.Read_Image.Root.GetFileSystemInfos();

                            Img.Volume_Label = Img.Read_Image.VolumeLabel;
                            //  Img.Volume_Box = Volume_List;
                            Img.Active_Variant = Img.Read_Image.ActiveVariant.ToString();
                            Img.HasBootIMage = Img.Read_Image.HasBootImage;
                            Img.IsThreadSafe = Img.Read_Image.IsThreadSafe;
                            Img.Root_Dir = Img.Read_Image.Root.Name;
                            Img.Cluster_Count = Img.Read_Image.TotalClusters;
                            //  Img.AFS_LIST = LV_AFS;
                            // Img.Folder_View = TV_FOLDERS;

                            //   LBL_Strip.Text = Img.Image_Path;

                            Array.Resize(ref Img.Root_Nodes, Img.Root_FSys_Info.Length);
                            Array.Resize(ref Root_Files, Img.Root_FSys_Info.Length);



                            byte[] sig_buffer = new byte[5];
                            fs.Seek(0x8000, SeekOrigin.Begin); // seek to PVD

                            PrimaryVolObj.type_flag = br.ReadByte();
                            PrimaryVolObj.STD_ID = System.Text.Encoding.ASCII.GetString(br.ReadBytes(sig_buffer.Length), 0, sig_buffer.Length);
                            

                            fs.Seek(0x8050, SeekOrigin.Begin);

                            PrimaryVolObj.Volume_Space = br.ReadInt32();

                            fs.Seek(0x8080, SeekOrigin.Begin);
                            PrimaryVolObj.LogicalBlockSz = br.ReadInt16();



                            for (int t = 0; t < Img.Physical_VolumeInfo.Length; t++)
                            {
                                Img.Physical_Size = Img.Physical_VolumeInfo[t].Length;
                            }

                            for (int i = 0; i < Img.Root_FSys_Info.Length; i++)
                            {

                                Debug_Log.AppendText("\nFSys: [ " + i.ToString() + "] " + Img.Root_FSys_Info[i].Name + " Attr " + Img.Root_FSys_Info[i].Attributes.ToString());

                                if (Img.Root_FSys_Info[i].Attributes.ToString().Contains("Directory"))
                                {
                                    Dir_Count++;

                                    Vol_List.Add(Img.Root_FSys_Info[i].Name);

                                    for (int x = 0; x < Img.RootDirectory_Info.Length; x++)
                                    {
                                        Img.Root_Nodes[i] = new TreeNode(Img.Root_FSys_Info[i].Name);

                                        Img.Root_Nodes[i].Nodes.Add(CreateDirNode(Img.RootDirectory_Info[x]));

                                        Img.Root_Nodes[i].ForeColor = GdiColor.White;
                                    }


                                }
                                else if (!Img.Root_FSys_Info[i].Attributes.ToString().Contains("Directory"))
                                {
                                    File_Count++;


                                    sstream = Img.Read_Image.GetFileInfo(Img.Root_FSys_Info[i].FullName).Open(FileMode.Open);

                                    br = new BinaryReader(sstream);

                                    sstream.Seek(0, SeekOrigin.Begin);


                                    // Set struct nodes to file name
                                    Img.Root_Nodes[i] = new TreeNode(Img.Root_FSys_Info[i].Name);
                                    Vol_List.Add(Img.Root_FSys_Info[i].Name);


                                    // if the files have a valid afs signature color code them..
                                    if (AFSIO.AfsValid(sstream, br))
                                    {
                                        Img.Root_Nodes[i].ForeColor = GdiColor.LimeGreen;

                                    }
                                    else
                                    {
                                        Img.Root_Nodes[i].ForeColor = GdiColor.IndianRed;
                                    }

                                }


                            }
                            

                            Debug_Log.AppendText("\b [DEBUG] Total Directories: " + Dir_Count.ToString() + "\n Root_File_Count: " + File_Count.ToString());

                        }
                        else
                            MessageBox.Show("Invalid Signature.. ", "Invalid iso9660", MessageBoxButtons.OK);

                    }
                    
                    foreach (var node in Img.Root_Nodes)
                    {
                        tv_root.Nodes.Add(node);

                    }


                }
                
            }

            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private static TreeNode CreateDirNode(DiscUtils.DiscDirectoryInfo directoryInfo)
        {
            // create a new node using the passed dir name
            var directoryNode = new TreeNode(directoryInfo.Name);


            foreach (var directory in directoryInfo.GetDirectories())
            {
                //   MessageBox.Show(directory.Attributes.ToString());

                // recursively add 
                directoryNode.Nodes.Add(CreateDirNode(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            }
            
            return directoryNode;
        }


        private void readImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AFS_LV.Items.Clear();
            tv_root.Nodes.Clear();
            Read_Image();
            
        }

        private void tv_root_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            int index = e.Node.Index;
            int afs_sig = 5457473;
            int afs_count = 0;

            tv_root.SelectedNode = null;


            // if file exists..
            if (File.Exists(Img.Image_Path))
            {

                // open file stream
                using (FileStream fs = new FileStream(Img.Image_Path, FileMode.Open))
                {
                    // if valid iso..
                    if (CDReader.Detect(fs))
                    {
                        Img.Read_Image = new CDReader(fs, true, true);
                        Img.Root_FSys_Info = Img.Read_Image.Root.GetFileSystemInfos();



                        Img.Selected_Volume = Img.Root_FSys_Info[index].FullName; // this
                        lbl_VolSel.Text = Img.Selected_Volume;
                        //  Img.Volume_Index = Img.Volume_Box.FindStringExact(Img.Selected_Volume);


                        //     Img.Volume_Box.SelectedIndex = Img.Volume_Index;


                        //   MessageBox.Show(MainForm.Img.Selected_Volume.ToString());
                        // open selected File for further parsing..
                        Stream memStream = Img.Read_Image.OpenFile(Img.Root_FSys_Info[index].FullName, FileMode.Open);
                        BinaryReader br = new BinaryReader(memStream);

                        memStream.Seek(0, SeekOrigin.Begin);




                        if (br.ReadInt32() == afs_sig && Img.Root_FSys_Info[index].Extension == "AFS")
                        {
                            //  MessageBox.Show("Valid Afs Archive..", "Valid", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            AFSIO.afs_parse(memStream, br, 0, AFS_LV, ProgressBar00, false, lbl_relativeOffset, Lbl_FCount, lbl_stats);

                        }

                        else if (Img.Root_FSys_Info[index].Name == "DATA.BIN")
                        {
                            AFSIO.data_bin_parse(memStream, br, AFS_LV, ProgressBar00, Lbl_FCount, lbl_stats);
                        }

                        else if (br.ReadInt32() != afs_sig)
                        {
                            MessageBox.Show("Invalid Afs Archive..", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            fs.Close();
                            br.Close();
                        }



                    }
                }
            }


        }

        private void AFS_LV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AFS_LV_ItemActivate(object sender, EventArgs e)
        {
            int index = AFS_LV.SelectedIndices[0];
            int vol_index = Img.Volume_Index; // u need to pass this

            int sel_off = int.Parse(AFS_LV.FocusedItem.SubItems[1].Text);


            if (File.Exists(Img.Image_Path))
            {
                using (FileStream fs = new FileStream(Img.Image_Path, FileMode.Open))
                {
                    if (Valid_Iso(fs))
                    {

                        Img.Read_Image = new CDReader(fs, true, true);
                        Img.Root_FSys_Info = Img.Read_Image.Root.GetFileSystemInfos();
                        Stream memStream = Img.Read_Image.OpenFile(Img.Selected_Volume, FileMode.Open); // IMG.selected volume wont update correctly, using the quicker volume browsing..
                        BinaryReader br = new BinaryReader(memStream);

                        int sel_siz = int.Parse(AFS_LV.Items[AFS_LV.SelectedIndices[0]].SubItems[2].Text);
                        
                        if (AFS_LV.Items[AFS_LV.SelectedIndices[0]].SubItems[3].Text.ToUpper().Contains("AFS"))
                        {

                            lbl_VolSel.Text = AFS_LV.Items[AFS_LV.SelectedIndices[0]].SubItems[3].Text;
                            

                            if (lbl_VolSel.Text.ToUpper() == "MIB.AFS" || lbl_VolSel.Text.ToUpper() == "EM_CMD.AFS")
                            {
                                AFSIO.afs_parse(memStream, br, sel_off, AFS_LV, ProgressBar00, true, lbl_relativeOffset, Lbl_FCount, lbl_stats);
                            }else
                            {
                                AFSIO.afs_parse(memStream, br, sel_off, AFS_LV, ProgressBar00, false, lbl_relativeOffset, Lbl_FCount, lbl_stats);
                            }



                            
                          
                        }
                        else
                        {
                          
                            memStream.Close();
                            br.Close();
                            fs.Close();

                        }

                        lbl_stats.Text = "Done";
                    }
                }
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 AbBox = new AboutBox1();

            
            AbBox.ShowInTaskbar = false;
            AbBox.ShowDialog();
            AbBox.TopMost = true;
        }

        private void CMB_AFS_Click(object sender, EventArgs e)
        {

        }

        private void enableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugForm DbgView = new DebugForm();            
            DbgView.ShowDialog();

        }

        private void sldunpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selected_file_count = 0;

            List<int> file_sizes = new List<int>();
            List<string> file_names = new List<string>();
            List<int> file_offsets = new List<int>();
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.Description = "Select Export Location...";
            FBD.ShowDialog();

            lbl_stats.Text = "Decompressing...";


            for (int i = 0; i < AFS_LV.SelectedItems.Count; i++)
            {
                selected_file_count++;
                file_offsets.Add(int.Parse(AFS_LV.Items[AFS_LV.SelectedIndices[i]].SubItems[1].Text));
                file_sizes.Add(int.Parse(AFS_LV.Items[AFS_LV.SelectedIndices[i]].SubItems[2].Text));

                file_names.Add(AFS_LV.Items[AFS_LV.SelectedIndices[i]].SubItems[3].Text);
            }

            ProgressBar00.Maximum = selected_file_count;

            int afsOffset = int.Parse(lbl_relativeOffset.Text);

            if (File.Exists(Img.Image_Path))
            {
                using (FileStream fs = new FileStream(Img.Image_Path, FileMode.Open))
                {
                    if (Valid_Iso(fs))
                    {
                        Img.Read_Image = new CDReader(fs, true, true);
                        Img.Root_FSys_Info = Img.Read_Image.Root.GetFileSystemInfos();
                        Stream memStream = Img.Read_Image.OpenFile(Img.Selected_Volume, FileMode.Open); // IMG.selected volume wont update correctly, using the quicker volume browsing..


                        if (selected_file_count > 1)
                        {
                            // run multi decompress op
                            for (int x = 0; x < selected_file_count; x++)
                            {
                                ProgressBar00.Value = x;

                                switch (AFS_LV.FocusedItem.SubItems[3].Text.Substring(AFS_LV.FocusedItem.SubItems[3].Text.Length - 3, 3))
                                {
                                    case "tm2": AFSIO.SLD_UNPACK(memStream, AFS_LV, AFS_LV.SelectedIndices[x], afsOffset, file_sizes[x], FBD.SelectedPath + "\\" + file_names[x].Substring(0, file_names[x].Length - 3) + "tm2", lbl_stats); break;

                                    case "bin": AFSIO.SLD_UNPACK(memStream, AFS_LV, AFS_LV.SelectedIndices[x], afsOffset, file_sizes[x], FBD.SelectedPath + "\\" + file_names[x].Substring(0, file_names[x].Length - 3) + "bin", lbl_stats); break;
                                }

                                

                                if (x == selected_file_count - 1)
                                {
                                    MessageBox.Show("Decompression Complete @ " + FBD.SelectedPath, "BOO YAH", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    ProgressBar00.Value = 0;
                                    lbl_stats.Text = Img.Image_Path;
                                }
                            }


                        }
                        // run single decompress op
                        else if (selected_file_count == 1)
                        {
                            int sel_off = int.Parse(AFS_LV.Items[AFS_LV.SelectedIndices[0]].SubItems[1].Text);
                            int sel_sz = int.Parse(AFS_LV.Items[AFS_LV.SelectedIndices[0]].SubItems[2].Text);


                          //  MessageBox.Show(AFS_LV.FocusedItem.SubItems[3].Text.Substring(AFS_LV.FocusedItem.SubItems[3].Text.Length - 3, 3));

                            switch (AFS_LV.FocusedItem.SubItems[3].Text.Substring(AFS_LV.FocusedItem.SubItems[3].Text.Length - 3, 3))
                            {
                                case "tm2": AFSIO.SLD_UNPACK(memStream, AFS_LV, AFS_LV.SelectedIndices[0], afsOffset, sel_sz, FBD.SelectedPath + "\\" + AFS_LV.FocusedItem.SubItems[3].Text.Substring(0, AFS_LV.FocusedItem.SubItems[3].Text.Length - 3) + "tm2", lbl_stats); break;

                                case "bin": AFSIO.SLD_UNPACK(memStream, AFS_LV, AFS_LV.SelectedIndices[0], afsOffset, sel_sz, FBD.SelectedPath + "\\" + AFS_LV.FocusedItem.SubItems[3].Text.Substring(0, AFS_LV.FocusedItem.SubItems[3].Text.Length - 3) + "bin", lbl_stats); break;
                            }

                            
                        }



                        memStream.Close();

                    }

                }

            }
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font_DLg.ShowApply = true;
            Font_DLg.ShowDialog();
        }

        private void Font_DLg_Apply(object sender, EventArgs e)
        {
            //Font_DLg.ShowColor = true;
         
            AFS_LV.Font = Font_DLg.Font;
          
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {



                string input = Interaction.InputBox("Direct File Search", "File Search");
                List<string> fnames = new List<string>();

                for (int i = 0; i < AFS_LV.Items.Count; i++)
                {

                    if (AFS_LV.Items[i].SubItems[3].Text.Contains(input))
                    {
                        AFS_LV.Items[i].EnsureVisible();
                        AFS_LV.Items[i].Selected = true;
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void refreshAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AFS_LV.Items.Clear();
            tv_root.Nodes.Clear();
        }
    }
}