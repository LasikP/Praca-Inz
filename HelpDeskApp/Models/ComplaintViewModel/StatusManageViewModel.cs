using System.Collections.Generic;

namespace HelpDeskApp.Models.ComplaintViewModel
{
    public class StatusManageViewModel
    {
        public StatusUpdateViewModel StatusUpdateViewModel { get; set; }
        public List<Comment> CommentList { get; set; }
    }
}
