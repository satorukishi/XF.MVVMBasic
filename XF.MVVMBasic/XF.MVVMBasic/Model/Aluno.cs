using System;
using System.Collections.Generic;
using System.Text;

namespace XF.MVVMBasic.Model
{
    public class Aluno
    {
        public Guid Id { get; set; }
        public string RM { get; set; }
        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                App.AlunoVM.OnAdicionarCMD.AdicionarCanExecuteChanged(); 
            }
        }

        public string Email { get; set; }
    }
}
