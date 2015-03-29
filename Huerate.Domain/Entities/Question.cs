/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huerate.Domain.Entities
{
    public abstract class Question : IPrimaryKeyEntity<int>, IOrderableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public int Order { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        public virtual RestaurantAccount Owner { get; set; }
    }

    [Table("YesNoQuestions")]
    public class YesNoQuestion : Question
    {
        public YesNoQuestion()
        {
            YesNoAnswers = new HashSet<YesNoAnswer>();
        }

        [ForeignKey("YesNoQuestionsFormStep")]
        public int? YesNoQuestionsFormStepId { get; set; }
        public virtual YesNoQuestionsFormStep YesNoQuestionsFormStep { get; set; }

        public virtual ICollection<YesNoAnswer> YesNoAnswers { get; set; }
    }

    [Table("PercentQuestions")]
    public class PercentQuestion : Question
    {
        public PercentQuestion()
        {
            PercentAnswers = new HashSet<PercentAnswer>();
        }

        [ForeignKey("PercentQuestionsFormStep")]
        public int? PercentQuestionsFormStepId { get; set; }
        public virtual PercentQuestionsFormStep PercentQuestionsFormStep { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<PercentAnswer> PercentAnswers { get; set; }
    }
}