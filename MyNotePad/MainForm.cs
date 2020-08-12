using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyNotePad
{
    public partial class form1 : Form
    {
        #region Field
        public static string filename="";
        public static bool ischanged = false;
        public static string default_name = "제목 없음 - 메모장";
        #endregion

        public form1()
        {
            InitializeComponent();
        }

        private void 새로만들기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!ischanged)
            {
                textBox.Clear();
                this.Text = default_name;
            }
            else
            {
                if(this.Text == default_name)
                {
                    Ask_Save f = new Ask_Save();
                    DialogResult ret = f.ShowDialog();

                    if (ret == DialogResult.OK)
                    {
                        saveFileDialog1.Filter = "텍스트문서(*.txt)|*.txt|모든파일|*.*";
                        saveFileDialog1.FileName = "*.txt";
                        saveFileDialog1.ShowDialog();

                        //파일을 저장한다.
                        File.WriteAllText(saveFileDialog1.FileName, textBox.Text);

                        textBox.Clear();
                        this.Text = default_name;
                    }
                    else if (ret == DialogResult.No)
                    {
                        textBox.Clear();
                        this.Text = default_name;
                    }
                    else if (ret == DialogResult.Cancel) { }
                }
                else
                {
                    File.WriteAllText(this.Text, textBox.Text);
                    textBox.Clear();
                    this.Text = default_name;
                }
            }
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1. 사용자에게 열 파일을 선택하게함
            openFileDialog1.Filter = "텍스트문서(*.txt)|*.txt|모든파일|*.*";
            openFileDialog1.ShowDialog();
            
            //취소를한다면 어떻게 처리할까
            filename = openFileDialog1.FileName;

            //2. 파일의 내용을 읽는다.
            textBox.Text = File.ReadAllText(openFileDialog1.FileName);
            //3. 화면에 표시한다.
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if(filename == "")
            {
                // 사용자에게 저장할 파일을 선택하게함
                saveFileDialog1.Filter = "텍스트문서(*.txt)|*.txt|모든파일|*.*";
                saveFileDialog1.ShowDialog();
                

                //파일을 저장한다.
                File.WriteAllText(saveFileDialog1.FileName, textBox.Text);

                filename = saveFileDialog1.FileName;
            }
            else
            {
                //해당 파일명으로 현재 내용을 저장
                File.WriteAllText(filename, textBox.Text);

            }
        }

        private void 다른이름으로저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 사용자에게 저장할 파일을 선택하게함
            saveFileDialog1.Filter = "텍스트문서(*.txt)|*.txt|모든파일|*.*";
            saveFileDialog1.ShowDialog();
            //파일을 저장한다.
            File.WriteAllText(saveFileDialog1.FileName, textBox.Text);
        }

        private void 끝내기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //프로그램 종료
            Close();
        }

        private void 자동줄바꿈ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.WordWrap = !textBox.WordWrap;
        }

        private void 글꼴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            textBox.Font = fontDialog1.Font;
        }

        private void 상태표시줄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusBar.Visible = 상태표시줄ToolStripMenuItem.Checked;
        }

        private void 메모장정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAbout help = new fAbout();
            help.ShowDialog();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            ischanged = true;
        }
    }
}
