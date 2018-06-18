using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XF.MVVMBasic.Model;
using XF.MVVMBasic.View;

namespace XF.MVVMBasic.ViewModel
{
    public class AlunoViewModel : INotifyPropertyChanged
    {
        #region Propriedades
        public Aluno AlunoModel { get; set; }
        public ObservableCollection<Aluno> Alunos { get; set; } = new ObservableCollection<Aluno>();

        // UI Events
        public OnAdicionarAlunoCMD OnAdicionarCMD { get; }
        public OnRemoverAlunoCMD OnRemoverCMD { get; }
        public ICommand OnNovoCMD { get; private set; }
        public ICommand OnSairCMD { get; private set; }
        #endregion

        public AlunoViewModel()
        {
            OnAdicionarCMD = new OnAdicionarAlunoCMD(this);
            OnRemoverCMD = new OnRemoverAlunoCMD(this);
            OnSairCMD = new Command(OnSair);
            OnNovoCMD = new Command(OnNovo);
        }

        public void Adicionar(Aluno paramAluno)
        {
            try
            {
                if (paramAluno == null)
                    throw new Exception("Aluno inválido");

                paramAluno.Id = Guid.NewGuid();
                Alunos.Add(paramAluno);
                App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                App.Current.MainPage.DisplayAlert("Ops", "Algo deu errado", "OK");
            }
        }

        public async void Remover(Aluno paramAluno)
        {
            try
            {
                if (paramAluno == null)
                {
                    throw new ArgumentException("Aluno não encontrado", "paramAluno");
                }

                bool removerAluno = await (App.Current.MainPage.DisplayAlert("Remover"
                                        , $"Confirma a exclusão do aluno: {paramAluno.Nome}"
                                        , "Sim", "Não"));

                if (removerAluno)
                {
                    Alunos.Remove(paramAluno);
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Ops", "Algo deu errado", "OK");
            }
        }

        private async void OnSair()
        {
            // Volta para a página que chamou esta janela
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnNovo()
        {
            App.AlunoVM.AlunoModel = new Aluno();
            await App.Current.MainPage.Navigation.PushAsync(new NovoAlunoView() { BindingContext = App.AlunoVM });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void EventPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class OnAdicionarAlunoCMD : ICommand
    {
        private AlunoViewModel alunoVM;
        public OnAdicionarAlunoCMD(AlunoViewModel paramVM)
        {
            alunoVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void AdicionarCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => (parameter != null) && (!string.IsNullOrWhiteSpace(((Aluno)parameter).Nome));
        public void Execute(object parameter)
        {
            alunoVM.Adicionar(parameter as Aluno);
        }
    }

    public class OnRemoverAlunoCMD : ICommand
    {
        private AlunoViewModel alunoVM;
        public OnRemoverAlunoCMD(AlunoViewModel paramVM)
        {
            alunoVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void DeleteCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => (parameter != null);
        public void Execute(object parameter)
        {
            alunoVM.Remover(parameter as Aluno);
        }
    }
}
