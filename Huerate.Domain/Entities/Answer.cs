/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huerate.Domain.Entities
{
    public abstract class Answer : IPrimaryKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public string Language { get; set; }

        [ForeignKey("AnswerSet")]
        public Guid AnswerSetId { get; set; }

        public virtual AnswerSet AnswerSet { get; set; }
    }

    [Table("YesNoAnswers")]
    public class YesNoAnswer : Answer
    {
        [ForeignKey("YesNoQuestion")]
        public int YesNoQuestionId { get; set; }
        public virtual YesNoQuestion YesNoQuestion { get; set; }

        public bool Value { get; set; }
    }

    [Table("PercentAnswers")]
    public class PercentAnswer : Answer
    {
        [ForeignKey("PercentQuestion")]
        public int PercentQuestionId { get; set; }
        public virtual PercentQuestion PercentQuestion { get; set; }

        public int Value { get; set; }
    }

    [Table("TextAnswers")]
    public class TextAnswer : Answer
    {
        [ForeignKey("TextQuestionFormStep")]
        public int TextQuestionFormStepId { get; set; }
        public virtual TextQuestionFormStep TextQuestionFormStep { get; set; }

        public string Value { get; set; }
    }
}
