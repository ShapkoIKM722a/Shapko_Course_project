using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Collections;

namespace kursova
{
    class MajorWork
    {
        private System.DateTime TimeBegin;
        private string Data;
        private string Result;
        public bool Modify;
        private int Key;

        public Stack myStack = new Stack();
        public string[] myArr = new string[100];

        public Queue myQueue = new Queue();
        public string[] smyQueue = new string[100];


        private string SaveFileName;
        private string OpenFileName;
        public void SaveToFile()
        {
            if (!this.Modify)
                return;
            try
            {
                Stream S;
                if (File.Exists(this.SaveFileName))
                    S = File.Open(this.SaveFileName, FileMode.Append);
                else
                    S = File.Open(this.SaveFileName, FileMode.Create);
                Buffer D = new Buffer();
                D.Data = this.Data;
                D.Result = Convert.ToString(this.Result);
                D.Key = Key;
                Key++;
                BinaryFormatter BF = new BinaryFormatter();
                BF.Serialize(S, D);
                S.Flush();
                S.Close();
                this.Modify = false;
            }
            catch
            {

                MessageBox.Show("Помилка роботи з файлом");
            }
        }
        public void ReadFromFile(System.Windows.Forms.DataGridView DG)
        {
            try
            {
                if (!File.Exists(this.OpenFileName))
                {
                    MessageBox.Show("Файлу немає");
                    return;
                }
                Stream S;
                S = File.Open(this.OpenFileName, FileMode.Open);
                Buffer D;
                object O;
                BinaryFormatter BF = new BinaryFormatter();
                
                System.Data.DataTable MT = new System.Data.DataTable();
                System.Data.DataColumn cKey = new System.Data.DataColumn("Ключ");
                System.Data.DataColumn cInput = new System.Data.DataColumn("Вхідні дані");
                System.Data.DataColumn cResult = new System.Data.DataColumn("Результат");

                MT.Columns.Add(cKey);
                MT.Columns.Add(cInput);
                MT.Columns.Add(cResult);

                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S);
                    D = O as Buffer;
                    if (D == null) break;
                    System.Data.DataRow MR;
                    MR = MT.NewRow();
                    MR["Ключ"] = D.Key;
                    MR["Вхідні дані"] = D.Data;
                    MR["Результат"] = D.Result;
                    MT.Rows.Add(MR);
                }
                DG.DataSource = MT;
                S.Close(); 
            }
            catch
            {
                MessageBox.Show("Помилка файлу");
            }
        }

        public void Find(string Num) 
        {
            int N;
            try
            {
                N = Convert.ToInt16(Num); 
            }
            catch
            {
                MessageBox.Show("помилка пошукового запиту"); 
              return;
            }

            try
            {
                if (!File.Exists(this.OpenFileName))
                {
                    MessageBox.Show("файлу немає"); 
                
                    return;
                }
                Stream S; 
                S = File.Open(this.OpenFileName, FileMode.Open); 
                Buffer D;
                object O;
                BinaryFormatter BF = new BinaryFormatter(); 
                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S);
                    D = O as Buffer;
                    if (D == null) break;
                    if (D.Key == N) 
{
                        string ST;
                        ST = "Запис містить:" + (char)13 + "No" + Num + "Вхідні дані:" +

                        D.Data + "Результат:" + D.Result;

                        MessageBox.Show(ST, "Запис знайдена");

                       S.Close();
                        return;
                    }
                }
                S.Close();
                MessageBox.Show("Запис не знайдена"); 
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); 
            }
        } 
        public void Generator() 
        {
            try
            {
                if (!File.Exists(this.SaveFileName)) 
                {
                    Key = 1;
                    return;
                }
                Stream S; 
                S = File.Open(this.SaveFileName, FileMode.Open); 
                Buffer D;
                object O; 
                BinaryFormatter BF = new BinaryFormatter(); 
                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S);
                    D = O as Buffer;
                    if (D == null) break;
                    Key = D.Key;
                }
                Key++;
                S.Close();
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); 
            }
        }
        public bool SaveFileNameExists()
        {
            if (this.SaveFileName == null)
                return false;
            else return true;
        }
        public void NewRec() 
        {
            this.Data = ""; 
            this.Result = null;
        }
        private string SaveTextFileName;
        public void WriteSaveTextFileName(string S)
        {
            this.SaveTextFileName = S;
        }
        public bool SaveTextFileNameExists()
        {
            if (this.SaveTextFileName == null)
                return false;
            else return true;
        }
        public string ReadSaveTextFileName()
        {
            return SaveTextFileName;
        }
        public void SaveToTextFile(string name, System.Windows.Forms.DataGridView D)
        {
            try
            {
                System.IO.StreamWriter textFile;
                if (!File.Exists(name))
                {
                    textFile = new System.IO.StreamWriter(name);
                }
                else
                {
                    textFile = new System.IO.StreamWriter(name, true);
                }
                for (int i = 0; i < D.RowCount - 1; i++)
                {
                    textFile.WriteLine("{0};{1};{2}", D[0, i].Value.ToString(), D[1,

                    i].Value.ToString(), D[2, i].Value.ToString());

                }
                textFile.Close();
            }
            catch
            {
                MessageBox.Show("Помилка роботи з файлом ");
            }
        }
        private string OpenTextFileName;
public void WriteOpenTextFileName(string S)
{
this.OpenTextFileName = S;
}
        public void WriteSaveFileName(string S)
        {
            this.SaveFileName = S;
        }
        public void WriteOpenFileName(string S)
        {
            this.OpenFileName = S;
        }
        public void SetTime() 
        {
            this.TimeBegin = System.DateTime.Now;
        }
        public System.DateTime GetTime() 
        {
            return this.TimeBegin;
        }
        public void Write(string D)
           {
            this.Data = D;
           }
           public string Read()
           {
            return this.Result;
           }
           public void Task() 
           {

            if (this.Data.Length != 5)
            {
                this.Result = Convert.ToString(false);
                return;
            }

            int firstTwoDigitsSum = int.Parse(this.Data[0].ToString()) + int.Parse(this.Data[1].ToString());
            int lastThreeDigitsSum = int.Parse(this.Data[2].ToString()) + int.Parse(this.Data[3].ToString()) + int.Parse(this.Data[4].ToString());

            if (firstTwoDigitsSum == lastThreeDigitsSum)
            {
                this.Result = Convert.ToString(true);
            }
            else
            {
                this.Result = Convert.ToString(false);
            }
            this.Modify = true;
        }
       }
}


