using CBT_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBT_WebApi.Models.DTOs
{
    public class CandidateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
    }

    public class CandidateDtoForUpdate
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }

    public class CandidateTestScoreDto
    {
        [Required]
        public int CandidateId { get; set; }
        [Required]
        public int TestScore { get; set; }
        [Required]
        public string TestName { get; set; }
    }

    public class CandidateTestScores
    {
        public Candidate CandidateInfo { get; set; }
        public List<TestScore> TestScores { get; set; }
    }
}
