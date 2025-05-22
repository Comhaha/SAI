using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guna.UI2.WinForms;

namespace SAI.App.Views.Common
{
    internal class TodoManager
    {
        private readonly List<(Guna2PictureBox todo, Guna2PictureBox done)> todoPairs;

        public TodoManager(params (Guna2PictureBox todo, Guna2PictureBox done)[] pairs)
        {
            todoPairs = new List<(Guna2PictureBox, Guna2PictureBox)>(pairs);
        }

        // index 번째 ToDo 상태 설정: true = 진행 중, false = 완료
        public void SetTodoStatus(int index, bool isActive)
        {
            if (index < 0 || index >= todoPairs.Count) return;

            todoPairs[index].todo.Visible = isActive;
            todoPairs[index].done.Visible = !isActive;
        }

        // 전체 상태 초기화
        public void SetAllStatus(bool[] statusArray)
        {
            for (int i = 0; i < todoPairs.Count && i < statusArray.Length; i++)
            {
                SetTodoStatus(i, statusArray[i]);
            }
        }
    }
}
