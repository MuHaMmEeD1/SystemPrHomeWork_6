using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practika1.Models
{
    public class Comment
    {

        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }

        public override string ToString()
        {
            return $"{postId} {id} {name} {email} {body} \n";
        }
    }
}
