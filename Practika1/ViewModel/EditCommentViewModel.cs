using Practika1.Models;
using Practika1.RCommand;
using Practika1.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practika1.ViewModel
{
    public class EditCommentViewModel
    {
        public EditCommentView EditCommentView { get; set; }

        public int Id { get; set; }

        public RealyyCommand EditCommand { get; set; }
        public RealyyCommand CancelCommand { get; set; }
        public bool Yoxla = true;
        public List<Comment> Comments = new(); 

        public EditCommentViewModel(EditCommentView editCommentView, int Id)
        {
            EditCommentView = editCommentView;
            this.Id = Id;


            CancelCommand = new RealyyCommand((object? par) => { EditCommentView.Close(); }, (object? par) => { return Yoxla; });
            EditCommand = new RealyyCommand(EditCommandFunction, CanEditCommandFunction);


            Comments = JsonSerializer.Deserialize<List<Comment>>(File.ReadAllText("../../../Text1.json"));

            foreach (var com in Comments)
            {
                if (com.id == Id)
                {
                    EditCommentView.IdTextBox.Text = com.postId.ToString();
                    EditCommentView.NameTextBox.Text = com.name;
                    EditCommentView.EmailTextBox.Text = com.email;
                    EditCommentView.BodyTextBox.Text = com.body;
                }
            }

        }

        public async Task EditFunction()
        {

            await Task.Delay(2000);

            foreach (var com in Comments)
            {
                if(com.id == Id)
                {
                    com.postId = int.Parse(EditCommentView.IdTextBox.Text.ToString());
                    com.name = EditCommentView.NameTextBox.Text;
                    com.email = EditCommentView.EmailTextBox.Text;
                    com.body = EditCommentView.BodyTextBox.Text;
                }
            }



            File.WriteAllText("../../../Text1.json", JsonSerializer.Serialize(Comments));
            EditCommentView.Close();

        }

        public void EditCommandFunction(object? par)
        {
            Yoxla = false;
            _ = EditFunction();

        }
        public bool CanEditCommandFunction(object? par)
        {
            if (EditCommentView.IdTextBox.Text == "" || EditCommentView.NameTextBox.Text == "" || EditCommentView.EmailTextBox.Text == "" || EditCommentView.BodyTextBox.Text == "" || !Regex.IsMatch(EditCommentView.IdTextBox.Text, @"^\d+$"))
            {
                return false;
            }
            return true;
        }


    }
}
