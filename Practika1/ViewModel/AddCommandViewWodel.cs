using Practika1.Models;
using Practika1.RCommand;
using Practika1.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Practika1.ViewModel
{
    public class AddCommandViewWodel
    {

        public AddCommentView AddCommentView { get; set; }

        public RealyyCommand AddCommand { get; set; }
        public RealyyCommand CanleCommand { get; set; }
        public bool Yoxla = true;


        public AddCommandViewWodel(AddCommentView addCommentView)
        {
            AddCommentView = addCommentView;

            AddCommand = new RealyyCommand(AddCommandFunction, CanAddCommandFunction);
            CanleCommand = new RealyyCommand((object? par) => { AddCommentView.Close(); }, (object? par) => { return Yoxla; });
        }


        public async Task AddFunction()
        {
            await Task.Delay(2000);
            var comments = JsonSerializer.Deserialize<List<Comment>>(File.ReadAllText("../../../Text1.json"));

            comments.Add(new Comment()
            {
                postId = int.Parse(AddCommentView.IdTextBox.Text.ToString()),
                id = comments[comments.Count - 1].id + 1,
                name = AddCommentView.NameTextBox.Text,
                email = AddCommentView.EmailTextBox.Text,
                body = AddCommentView.BodyTextBox.Text

            }) ;

            File.WriteAllText("../../../Text1.json", JsonSerializer.Serialize(comments));
            AddCommentView.Close();
        }

        public void AddCommandFunction(object? par)
        {
            Yoxla = false;
            _ = AddFunction();
        }
        public bool CanAddCommandFunction(object? par)
        {
            if (AddCommentView.IdTextBox.Text == "" || AddCommentView.NameTextBox.Text == "" || AddCommentView.EmailTextBox.Text == "" || AddCommentView.BodyTextBox.Text == "" || !Regex.IsMatch(AddCommentView.IdTextBox.Text, @"^\d+$"))
            {
                return false;
            }
            return true;
        }







    }
}
