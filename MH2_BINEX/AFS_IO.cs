using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MH2_BINEX
{
    public class AFS_IO
    {




        public Dictionary<string, string> LUT_EXTENSIONS = new Dictionary<string, string>()
        {
            {"AFS", "Archived File System" },
            {"TM2", "Playstation Texture"},
            {"AMO", "Animated Mesh/Model Object?"},
            {"SFD", "Softdec mpeg video"},
            {"ADX", "Adx sound clip"},
            {"SND", "Sound Container" },
            {"MIB", "Mission/Quest Text?" },
            {"HIG", "Enemy Related?"},
            {"TEX", "Texture Container"},
            {"AHI", "Skeletal Hierarchy?"},
            

        };

        public Dictionary<string, Color> LUT_FMTCOLOR = new Dictionary<string, System.Drawing.Color>
        {
            {"AFS", Color.LightSeaGreen},
            {"TM2", Color.DarkSeaGreen},
            {"AMO", Color.PowderBlue},
            {"SFD", Color.MistyRose},
            {"ADX", Color.Yellow},
            {"SND", Color.Orange},
            {"MIB", Color.CornflowerBlue},
            {"HIG", Color.Aquamarine},
            {"TEX", Color.Honeydew},
            {"AHI", Color.Bisque }
        };


        public struct AFS_HEADER_OBJ
        {
            public Int32 offset;
            public Int32 size;
            public string Name;
            
        }


        public struct SUB_AFS_OBJ
        {
            public Int32 offset;
            public Int32 size;
            public string name;

        }

        public struct date_time_obj
        {
            public Int16 Year;
            public Int16 Month;
            public Int16 Day;
            public Int16 Hour;
            public Int16 Minute;
            public Int16 Second;
            public DateTime DT;
            public Int32 f_size;
        }

     
        public date_time_obj[] DATE_TIME = new date_time_obj[0];
        public AFS_HEADER_OBJ[] AFS_HEADER = new AFS_HEADER_OBJ[0];

        public string export_file_path = string.Empty;

        /// <summary>
        /// Export Selected Files From Listview..
        /// </summary>
        /// <param name="stream">Current Volume Stream..</param>
        /// <param name="lv">Listview Control were exporting from..</param>
        /// <param name="sel_count">Total Files Selected..</param>
        /// <param name="start_off">Starting offset of volume..</param>
        /// <param name="Sel_Off">List of Selected File Offsets</param>
        /// <param name="Sel_Sz">List of Selected File Sizes</param>
        /// <param name="Sel_File">List of Selected File Names</param>
        public void Export_SelectedFIles(Stream stream, ListView lv, int sel_count, int start_off, int RelativeOff, List<int> Sel_Off, List<int> Sel_Sz, List<string> Sel_File, bool data_bin, ToolStripProgressBar BufferBar, ToolStripLabel Status)
        {
            try
            {
                // open binary reader
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                FBD.Description = "Select Output Destination for Selected Files..";
                FBD.ShowDialog();

                BufferBar.Maximum = sel_count;

                for (int i = 0; i < sel_count; i++)
                {
                    BufferBar.Value = i;

                    BinaryReader br = new BinaryReader(stream);

                    string output = FBD.SelectedPath + "\\" + Sel_File[i];
                    byte[] buffer = new byte[Sel_Sz[i]];

                    int file_off = Sel_Off[i];

                    if (data_bin)
                    {
                        stream.Seek(file_off + RelativeOff, SeekOrigin.Begin);
                    }
                    else
                    {
                        stream.Seek(file_off, SeekOrigin.Begin);
                    }
                 
                
                    br.Read(buffer, 0, buffer.Length);


                    br.Close();


                    FileStream OutputStream = new FileStream(output, FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(OutputStream);

                    bw.Write(buffer, 0, buffer.Length);


                    OutputStream.Close();
                    bw.Close();

                    Status.Text = "Succesful Export to :" + output;
                    
                }

                

                stream.Close();

            }

            catch
            {

            }


        }

        /// <summary>
        /// Export a Single file using the given file stream
        /// </summary>
        /// <param name="stream"> This stream to read from </param>
        /// <param name="LV"> Listview Control</param>
        /// <param name="start_off">The volume relative start offset </param>
        /// <param name="t_sz"> The Total size of selected file </param>
        public string Export_File(Stream stream, ListView LV, int start_off, int relative_offset, int t_sz, bool data_bin, ToolStripLabel Status)
        {
            try
            {
                int index = LV.SelectedIndices[0];
                int file_off = 0;

                BinaryReader br = new BinaryReader(stream); // open binary reader
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                FBD.Description = "Select Output Destination for Selected Files..";
                FBD.ShowDialog();

                export_file_path = FBD.SelectedPath;
                string selected_file = LV.Items[index].SubItems[3].Text;

                string output = export_file_path + "\\" + selected_file;

                byte[] buffer = new byte[t_sz];


                //    file_off = int.Parse(LV.Items[index].SubItems[1].Text) + start_off;


                if (data_bin)
                {
                    stream.Seek(start_off + relative_offset, SeekOrigin.Begin);
                }
                else
                {
                    stream.Seek(start_off, SeekOrigin.Begin);
                }


                
                br.Read(buffer, 0, buffer.Length);


                stream.Close();
                br.Close();

                FileStream OutputStream = new FileStream(output, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(OutputStream);

                bw.Write(buffer, 0, buffer.Length);

                


                OutputStream.Close();
                bw.Close();

                Status.Text = "Succesful Export to : " + output;

                return output;

            }

            catch (System.UnauthorizedAccessException e)
            {
                MessageBox.Show("\nIgnore\n" + e.Message.ToString());
                return string.Empty;
            }


        }


        public void afs_parse(Stream fs, BinaryReader br, int start_off, ListView LV_Archive, ToolStripProgressBar BufferBar, bool data_bin, ToolStripLabel relativeOffset, ToolStripLabel FCount, ToolStripLabel Status)
        {
            
             Int32 fmt_id = 0;
            Int32 afs_count = 0;
            byte[] buffer = new byte[32];
            string[] fnames = new string[0];
            List<string> File_Names = new List<string>();
            List<Int32> Entry_Offsets = new List<Int32>();
            List<Int32> Entry_Sizes = new List<Int32>();

                     
            int Table_Index = 0;
            int string_pos = 0;
            
            LV_Archive.Items.Clear();


            int relevant_offset = 0;


            if (data_bin)
            {
                relevant_offset = start_off + 524288;

              //  MessageBox.Show("Relevant Offset: " + relevant_offset.ToString());
                fs.Seek(relevant_offset, SeekOrigin.Begin);
                
            }
            else
            {
              //  MessageBox.Show("nond ata bin " + relevant_offset.ToString());
                relevant_offset = start_off;
                fs.Seek(relevant_offset, SeekOrigin.Begin);
            }



            relativeOffset.Text =  relevant_offset.ToString();
            fmt_id = br.ReadInt32();
            afs_count = br.ReadInt32();

            FCount.Text = "Total Files: " + afs_count.ToString();

            BufferBar.Maximum = afs_count;
           
            Array.Resize(ref AFS_HEADER, afs_count);

           

            for (int i = 0; i < AFS_HEADER.Length; i++) // store offsets and size in struct array
            {
                AFS_HEADER[i].offset = br.ReadInt32();
                AFS_HEADER[i].size = br.ReadInt32();
                LV_Archive.Items.Add(i.ToString());
                LV_Archive.Items[i].ImageIndex = 0; // setting all to use image list index 0..
                LV_Archive.Items[i].SubItems.Add(AFS_HEADER[i].offset.ToString());
                LV_Archive.Items[i].SubItems.Add(AFS_HEADER[i].size.ToString());
           //     Entry_Offsets.Add(AFS_HEADER[i].offset);
           //     Entry_Sizes.Add(AFS_HEADER[i].size);

            }


         //  AFS_HEADER[Table_Index].offset - 8


            fs.Seek(relevant_offset + 524280, SeekOrigin.Begin);

            int TOC_LOC = br.ReadInt32();           
            int TOC_SZ = br.ReadInt32();



            fs.Seek(TOC_LOC + relevant_offset, SeekOrigin.Begin);

            Array.Resize(ref fnames, afs_count);
            Array.Resize(ref DATE_TIME, afs_count);


            for (int x = 0; x < afs_count; x++) // - 1 because the table doesent include itself in file list..
            {
                br.Read(buffer, 0, 32);

                for (int j = 0; j < buffer.Length; j++)
                {
                    if (buffer[j] == 0x00)
                    {
                        string_pos = j;
                        break; // without the break it just keeps running through all padding..
                    }
                }

                Status.Text = "Processing Afs...";


                string ext = System.Text.Encoding.ASCII.GetString(buffer, 0, string_pos);
                ext = ext.Substring(ext.Length - 3, 3);
                ext = ext.ToUpper();


                File_Names.Add(System.Text.Encoding.ASCII.GetString(buffer, 0, string_pos).Trim());
                AFS_HEADER[x].Name = File_Names[x];

                DATE_TIME[x].Year = br.ReadInt16();
                DATE_TIME[x].Day = br.ReadInt16();
                DATE_TIME[x].Month = br.ReadInt16();
                DATE_TIME[x].Hour = br.ReadInt16();
                DATE_TIME[x].Minute = br.ReadInt16();
                DATE_TIME[x].Second = br.ReadInt16();
                DATE_TIME[x].f_size = br.ReadInt32();
                DATE_TIME[x].DT = new DateTime(DATE_TIME[x].Year, DATE_TIME[x].Day, DATE_TIME[x].Month, DATE_TIME[x].Hour, DATE_TIME[x].Minute, DATE_TIME[x].Second);
                LV_Archive.Items[x].SubItems.Add(AFS_HEADER[x].Name.ToString());

                if (LUT_EXTENSIONS.ContainsKey(ext))
                {
                    LV_Archive.Items[x].SubItems.Add(LUT_EXTENSIONS[ext]);
                    LV_Archive.Items[x].ForeColor = LUT_FMTCOLOR[ext];
                }
                else
                {
                    LV_Archive.Items[x].SubItems.Add("Undefined");
                    LV_Archive.Items[x].ForeColor = Color.MediumVioletRed;
                }

                LV_Archive.Items[x].SubItems.Add(DATE_TIME[x].DT.ToString());

                BufferBar.Value = x;

              

            }


            Status.Text = "Done!";

            fs.Close();
            br.Close();
          

        }

        public void data_bin_parse(Stream fs, BinaryReader br, ListView LV, ToolStripProgressBar BufferBar, ToolStripLabel FCount, ToolStripLabel status)
        {
            Int32 fmt_id = 0;
            Int32 afs_count = 0;

           

            LV.Items.Clear();



            fs.Seek(0, SeekOrigin.Begin);

            fmt_id = br.ReadInt32();
            afs_count = br.ReadInt32();
            Array.Resize(ref AFS_HEADER, afs_count);
            BufferBar.Maximum = afs_count;
            FCount.Text = "Total Files: " + afs_count.ToString();

            for (int i = 0; i < AFS_HEADER.Length; i++) // store offsets and size in struct array
            {

                BufferBar.Value = i;
             

                AFS_HEADER[i].offset = br.ReadInt32();
                AFS_HEADER[i].size = br.ReadInt32();
                LV.Items.Add(i.ToString());
                LV.Items[i].ImageIndex = 0; // setting all to use image list index 0..
                LV.Items[i].SubItems.Add(AFS_HEADER[i].offset.ToString());
                LV.Items[i].SubItems.Add(AFS_HEADER[i].size.ToString());

                switch (i)
                {
                    case 0: LV.Items[i].SubItems.Add("Data.Afs"); break;
                    case 1: LV.Items[i].SubItems.Add("sub_main.bin"); break;
                }

                LV.Items[i].ForeColor = Color.White;
                
            }


            status.Text = "Done";

            fs.Close();
            br.Close();
        }


        public bool AfsValid(Stream stream, BinaryReader br)
        {
            int afs_sig = 5457473;

            stream.Seek(0, SeekOrigin.Begin);
            if (br.ReadInt32() == afs_sig)
            {
                return true;
            }
            else
                return false;

        }





        public void SLD_UNPACK(Stream fs, ListView LV, int LvIndex, int start_off, int fsz, string outputPath, ToolStripLabel Debug)
        {
            try
            {

                byte[] dest = new byte[1024 * 1024 * 4];
                int file_off = 0;
                int file_sz = 0;
                int index = LV.FocusedItem.Index;
                int total_items = LV.SelectedItems.Count;

                UInt32 mask, tmp, length;
                UInt32 offset;
                UInt16 seq;
                UInt32 dptr, sptr;
                UInt16 l, ct;
                UInt16 t, s;

                BinaryReader br = new BinaryReader(fs);



                file_off = int.Parse(LV.Items[LvIndex].SubItems[1].Text) + start_off;
                file_sz = file_off + fsz;

                //  MessageBox.Show("off: " + file_off.ToString());
                fs.Seek(file_off, SeekOrigin.Begin);
                dptr = 0;
                seq = br.ReadUInt16();

                //   Debug.AppendText("\n Sequence: " + seq.ToString());
                sptr = 0;


                while (br.BaseStream.Position != file_sz)
                {
                    mask = seq;

                    for (t = 0; t < 16; t++)
                    {
                        if ((mask & (1 << (15 - t))) != 0)
                        {
                            seq = br.ReadUInt16();

                            sptr = sptr + 2;
                            tmp = seq;

                            offset = (UInt32)(tmp & 0x07ff);
                            length = (UInt16)((tmp >> 11) & 0x1f);

                            offset = (UInt16)(offset * 2);
                            length = (UInt16)(length * 2);


                            //   Debug.AppendText("\n Offset: " + offset.ToString() + "\n Length: " + length.ToString());

                            if (length > 0)
                            {
                                for (l = 0; l < length; l++)
                                {
                                    dest[dptr] = dest[dptr - offset];
                                    dptr++;
                                    // MessageBox.Show(dest.Length.ToString() + "Dptr" + dptr.ToString() + " Offset: " + offset.ToString());

                                }
                            }
                            else
                            {
                                seq = br.ReadUInt16();
                                sptr = sptr + 2;
                                length = (ushort)(seq * 2);


                                for (s = 0; s < length; s++)
                                {
                                    dest[dptr] = dest[dptr - offset];
                                    dptr++;
                                }

                            }


                        }
                        else
                        {
                            if (br.BaseStream.Position != file_sz)
                            {
                                seq = br.ReadUInt16();
                                sptr = sptr + 2;
                                dest[dptr++] = (byte)(seq & 0xff);
                                dest[dptr++] = (byte)((seq >> 8) & 0xff);
                            }

                        }
                    }

                    if (br.BaseStream.Position != file_sz)
                    {
                        seq = br.ReadUInt16();
                    }

                }


                br.Close();


                FileStream tm2out = new FileStream(outputPath, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(tm2out);

                tm2out.Seek(0, SeekOrigin.Begin);

                int newPtr = int.Parse(dptr.ToString());

                bw.Write(dest, 0, newPtr);
                Debug.Text = "Decompress Succesful: @" + outputPath;
             //   Debug.ScrollToCaret();

                bw.Close();
                tm2out.Close();

                //   MessageBox.Show("DECOMPRESS SUCCESFUL @ " + outputPath);

                Debug.ForeColor = Color.Green;



            }
            catch(Exception ex)
            {
                MessageBox.Show("RIP" + ex.Message);
            }


        }


    }
    
}


