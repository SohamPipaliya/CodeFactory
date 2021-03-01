using System;

namespace CodeFactoryAPI.Models
{
    public class ReplyJsonModel
    {
        public ReplyJsonModel()
        {

        }

        public ReplyJsonModel(Reply reply)
        {
            Reply_ID = reply.Reply_ID;
            Message = reply.Message;
            Code = reply.Code;
            Image1 = reply.Image1;
            Image2 = reply.Image2;
            Image3 = reply.Image3;
            Image4 = reply.Image4;
            Image5 = reply.Image5;
            RepliedDate = reply.RepliedDate;
            User_ID = reply.User_ID;
            User = reply.User;
            Question_ID = reply.Question_ID;
            Question = reply.Question;
        }

        public Guid Reply_ID { get; set; }

        public string Message { get; set; }

        public string? Code { get; set; }

        public string? Image1 { get; set; }

        public string? Image2 { get; set; }

        public string? Image3 { get; set; }

        public string? Image4 { get; set; }

        public string? Image5 { get; set; }

        public DateTime RepliedDate { get; set; }

        public Guid? User_ID { get; set; }

        public User? User { get; set; }

        public Guid? Question_ID { get; set; }

        public QuestionJsonModel? Question { get; set; }

        public static implicit operator ReplyJsonModel(Reply reply) => new()
        {
            Reply_ID = reply.Reply_ID,
            Message = reply.Message,
            Code = reply.Code,
            Image1 = reply.Image1,
            Image2 = reply.Image2,
            Image3 = reply.Image3,
            Image4 = reply.Image4,
            Image5 = reply.Image5,
            RepliedDate = reply.RepliedDate,
            User_ID = reply.User_ID,
            User = reply.User,
            Question_ID = reply.Question_ID,
            Question = reply.Question
        };
    }

    public class QuestionJsonModel
    {
        public Guid Question_ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? Code { get; set; }

        public string? Image1 { get; set; }

        public string? Image2 { get; set; }

        public string? Image3 { get; set; }

        public string? Image4 { get; set; }

        public string? Image5 { get; set; }

        public DateTime AskedDate { get; set; }

        public Guid? User_ID { get; set; }

        public User? User { get; set; }

        public Guid? Tag1_ID { get; set; }

        public Tag? Tag1 { get; set; }

        public Guid? Tag2_ID { get; set; }

        public Tag? Tag2 { get; set; }

        public Guid? Tag3_ID { get; set; }

        public Tag? Tag3 { get; set; }

        public Guid? Tag4_ID { get; set; }

        public Tag? Tag4 { get; set; }

        public Guid? Tag5_ID { get; set; }

        public Tag? Tag5 { get; set; }

        public static implicit operator QuestionJsonModel(Question? jsonModel) => jsonModel is null ? null : new()
        {
            Question_ID = jsonModel.Question_ID,
            Title = jsonModel.Title,
            Description = jsonModel.Description,
            Code = jsonModel.Code,
            Image1 = jsonModel.Image1,
            Image2 = jsonModel.Image2,
            Image3 = jsonModel.Image3,
            Image4 = jsonModel.Image4,
            Image5 = jsonModel.Image5,
            AskedDate = jsonModel.AskedDate,
            User_ID = jsonModel.User_ID,
            User = jsonModel.User,
            Tag1_ID = jsonModel.Tag1_ID,
            Tag1 = jsonModel.Tag1,
            Tag2_ID = jsonModel.Tag2_ID,
            Tag2 = jsonModel.Tag2,
            Tag3_ID = jsonModel.Tag3_ID,
            Tag3 = jsonModel.Tag3,
            Tag4_ID = jsonModel.Tag4_ID,
            Tag4 = jsonModel.Tag4,
            Tag5_ID = jsonModel.Tag5_ID,
            Tag5 = jsonModel.Tag5
        };
    }
}
