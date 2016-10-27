using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal
{
    class Undo_Redo
    {
        struct UnRedo
        {
            string _var;
            int _position;
        }
        List<Stack<string>> Undo;
        List<Stack<string>> Redo;
        List<bool> CanUndo;
        List<bool> CanRedu;
       public Undo_Redo()
        {
            Undo = new List<Stack<string>>();
            Redo = new List<Stack<string>>();
            CanUndo = new List<bool>();
            CanRedu = new List<bool>();
            AddPage();
        }
        public void AddPage()
        {
            Undo.Add(new Stack<string>());
            Redo.Add(new Stack<string>());
            CanUndo.Add(false);
            CanRedu.Add(false);
        }
        public void RemovePage(int _index)
        {
            Undo.RemoveAt(_index);
            Redo.RemoveAt(_index);
            CanRedu.RemoveAt(_index);
            CanUndo.RemoveAt(_index);
        }
        public void AddToUndo(int _index, string _tabPage)
        {
            Undo[_index].Push(_tabPage);
            CanUndo[_index] = true;
        }
        public string GetUndo(int _index)
        {
            if (Undo[_index].Count > 0)
            {
                if (Undo[_index].Count == 1)
                    CanUndo[_index] = false;
                string _TP = Undo[_index].Pop();
                Redo[_index].Push(_TP);
                CanRedu[_index] = true;
                return _TP;
            }
            return "";
        }
        public string GetRedo(int _index)
        {
            if (Redo[_index].Count > 0)
            {
                if (Redo[_index].Count == 1)
                    CanRedu[_index] = false;
                string _TP = Redo[_index].Pop();
                Undo[_index].Push(_TP);
                return _TP;
            }
            return "";
        }
        public void ClearRedo(int _index)
        {
            Redo[_index].Clear();
            CanRedu[_index] = false;
        }
        public void ClearUndo(int _index)
        {
            Redo[_index].Clear();
            CanUndo[_index] = false;
        }
        public bool CheckUndo(int _index)
        {
            return CanUndo[_index];
        }
        public bool CheckRedu(int _index)
        {
            return CanRedu[_index];
        }

    }
}
