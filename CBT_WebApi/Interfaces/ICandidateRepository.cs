using CBT_WebApi.Models;
using CBT_WebApi.Models.DTOs;
using CBT_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBT_WebApi.Interfaces
{
    public interface ICandidateRepository
    {
        Task<GenericResponse<List<Candidate>>> GetCandidates();
        Task<GenericResponse<Candidate>> AddCandidate(CandidateDto candidateDto);
        Task<GenericResponse<Candidate>> Update(CandidateDtoForUpdate candidateDto);
        Task<GenericResponse<TestScore>> AddCandidateTestScore(CandidateTestScoreDto candidateDto);
        Task<GenericResponse<CandidateTestScores>> GetCandidateTestScores(int candidateId);
    }
}
