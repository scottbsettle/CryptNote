using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal
{
    public struct UnRedo
    {
        public string m_var;
        public int m_position;
        public UnRedo(string _var, int _pos)
        {
            m_var = _var;
            m_position = _pos;
        }
    }
    class Undo_Redo
    {
       
        List<Stack<UnRedo>> Undo;
        List<Stack<UnRedo>> Redo;
        List<bool> CanUndo;
        List<bool> CanRedu;
        UnRedo m_Buffer;
       public Undo_Redo()
        {
            Undo = new List<Stack<UnRedo>>();
            Redo = new List<Stack<UnRedo>>();
            CanUndo = new List<bool>();
            CanRedu = new List<bool>();
            AddPage();
        }
        public void AddPage()
        {
            Undo.Add(new Stack<UnRedo>());
            Redo.Add(new Stack<UnRedo>());
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
        public void AddToUndo(int _index, string _var, int _position)
        {
            m_Buffer = new UnRedo(_var, _position);
            Undo[_index].Push(m_Buffer);
            CanUndo[_index] = true;
        }
        public UnRedo GetUndo(int _index)
        {
                if (Undo[_index].Count == 1)
                    CanUndo[_index] = false;
                UnRedo _TP = Undo[_index].Pop();
                Redo[_index].Push(_TP);
                CanRedu[_index] = true;
                return _TP;
        }
        public UnRedo GetRedo(int _index)
        {
                if (Redo[_index].Count == 1)
                    CanRedu[_index] = false;
                UnRedo _TP = Redo[_index].Pop();
                Undo[_index].Push(_TP);
                return _TP;
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
