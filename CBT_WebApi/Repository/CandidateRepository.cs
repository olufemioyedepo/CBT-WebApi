using CBT_WebApi.Helper;
using CBT_WebApi.Interfaces;
using CBT_WebApi.Models;
using CBT_WebApi.Models.DTOs;
using CBT_WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBT_WebApi.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CbtAppDbContext cbtAppDbContext;
        public CandidateRepository(CbtAppDbContext appDbContext)
        {
            cbtAppDbContext = appDbContext;
        }
        public async Task<GenericResponse<List<Candidate>>> GetCandidates()
        {
            try
            {
                var candidates = await cbtAppDbContext.Candidates.ToListAsync();
                return new GenericResponse<List<Candidate>>()
                {
                    Code = 200,
                    Data = candidates,
                    Message = candidates.Count > 0 ? "Candidates loaded successfully!" : "No candidate record found!",
                    Success = true
                };

            }
            catch (Exception ex)
            {
                Log.Error(Util.LogError(ex));
                throw ex;
            }
        }

        public async Task<GenericResponse<Candidate>> AddCandidate(CandidateDto candidateDto)
        {
            try
            {
                var candidateObj = new Candidate()
                {
                    Email = candidateDto.Email,
                    Mobile = candidateDto.Mobile,
                    Name = candidateDto.Name
                };

                cbtAppDbContext.Candidates.Add(candidateObj);
                int recordsSaved = await cbtAppDbContext.SaveChangesAsync();

                if (recordsSaved > 0)
                {
                    return new GenericResponse<Candidate>()
                    {
                        Code = 201,
                        Data = candidateObj,
                        Message = "Candidate created successfully!",
                        Success = true
                    };
                }

                return new GenericResponse<Candidate>()
                {
                    Code = 200,
                    Data = null,
                    Message = "Candidate entry not created!",
                    Success = false
                };

            }
            catch (Exception ex)
            {
                Log.Error(Util.LogError(ex));
                return new GenericResponse<Candidate>()
                {
                    Code = 500,
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Candidate>> Update(CandidateDtoForUpdate candidateDtoForUpdate)
        {
            try
            {
                var candidate = await cbtAppDbContext.Candidates.Where(s => s.Id == candidateDtoForUpdate.Id).FirstOrDefaultAsync();
                if (candidate == null)
                {
                    return new GenericResponse<Candidate>()
                    {
                        Code = 404,
                        Data = null,
                        Message = $"No candidate found with the ID: {candidateDtoForUpdate.Id}",
                        Success = false

                    };
                }
                candidate.Name = candidateDtoForUpdate.Name;
                candidate.Email = candidateDtoForUpdate.Email;
                candidate.Mobile = candidateDtoForUpdate.Mobile;

                cbtAppDbContext.Entry(candidate).State = EntityState.Modified;
                int recordsUpdated = await cbtAppDbContext.SaveChangesAsync();

                if (recordsUpdated > 0)
                {
                    return new GenericResponse<Candidate>()
                    {
                        Code = 200,
                        Data = candidate,
                        Message = "Candidate information update was successfully!",
                        Success = true

                    };
                }

                return new GenericResponse<Candidate>()
                {
                    Code = 204,
                    Data = null,
                    Message = "Candidate information update failed, please try again!",
                    Success = false

                };
            }
            catch (Exception ex)
            {
                Log.Error(Util.LogError(ex));
                throw ex;
            }
        }

        public async Task<GenericResponse<TestScore>> AddCandidateTestScore(CandidateTestScoreDto candidateTestScoreDto)
        {
            try
            {
                var testScoreObj = new TestScore()
                {
                    CandidateId = candidateTestScoreDto.CandidateId,
                    Score = candidateTestScoreDto.TestScore,
                    Name = candidateTestScoreDto.TestName,
                    Date = DateTime.Now
                };

                cbtAppDbContext.TestScores.Add(testScoreObj);
                int recordsSaved = await cbtAppDbContext.SaveChangesAsync();

                if (recordsSaved > 0)
                {
                    return new GenericResponse<TestScore>()
                    {
                        Code = 201,
                        Data = testScoreObj,
                        Message = "Test score created successfully!",
                        Success = true
                    };
                }

                return new GenericResponse<TestScore>()
                {
                    Code = 200,
                    Data = null,
                    Message = "Test score entry not created!",
                    Success = false
                };

            }
            catch (Exception ex)
            {
                Log.Error(Util.LogError(ex));
                return new GenericResponse<TestScore>()
                {
                    Code = 500,
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<CandidateTestScores>> GetCandidateTestScores(int candidateId)
        {
            var candidateTestScore = new CandidateTestScores();
            var candidate = await cbtAppDbContext.Candidates.Where(s => s.Id == candidateId).FirstOrDefaultAsync();
            if (candidate == null)
            {
                return new GenericResponse<CandidateTestScores>()
                {
                    Code = 404,
                    Data = null,
                    Message = $"No candidate found with the ID: {candidateId}",
                    Success = false
                };
            }

            var testScores = await cbtAppDbContext.TestScores.Where(s => s.CandidateId == candidateId).OrderByDescending(s => s.Id).ToListAsync();
            candidateTestScore.CandidateInfo = candidate;
            candidateTestScore.TestScores = testScores;

            return new GenericResponse<CandidateTestScores>()
            {
                Code = 200,
                Data = candidateTestScore,
                Message = "Candidate test score retrieved!",
                Success = true
            };
        }
    }
}
