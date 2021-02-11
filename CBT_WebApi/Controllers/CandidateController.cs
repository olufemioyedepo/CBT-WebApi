using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBT_WebApi.Interfaces;
using CBT_WebApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CBT_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        ICandidateRepository _candidateRepository;
        

        public CandidateController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Get()
        {
            var candidates = await _candidateRepository.GetCandidates();
            return Ok(candidates);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CandidateDto candidateObj)
        {
            var candidate = await _candidateRepository.AddCandidate(candidateObj);
            if (candidate.Code == 201)
            {
                return StatusCode(StatusCodes.Status201Created, candidate);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, candidate);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put(CandidateDtoForUpdate candidateDtoForUpdate)
        {
            var candidate = await _candidateRepository.Update(candidateDtoForUpdate);
            if (candidate.Code == 200)
            {
                return StatusCode(StatusCodes.Status200OK, candidate);
            }

            if (candidate.Code == 404)
            {
                return StatusCode(StatusCodes.Status404NotFound, candidate);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, candidate);
        }

        [HttpPost]
        [Route("addtestscore")]
        public async Task<IActionResult> AddTestScore(CandidateTestScoreDto candidateTestScoreObj)
        {
            var candidateTestScore = await _candidateRepository.AddCandidateTestScore(candidateTestScoreObj);
            if (candidateTestScore.Code == 201)
            {
                return StatusCode(StatusCodes.Status201Created, candidateTestScore);
            }
            
            if (candidateTestScore.Code == 200)
            {
                return StatusCode(StatusCodes.Status200OK, candidateTestScore);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, candidateTestScore);
        }

        [HttpGet]
        [Route("testscores/{candidateId}")]
        public async Task<IActionResult> GetTestScores(int candidateId)
        {
            var candidateTestScores = await _candidateRepository.GetCandidateTestScores(candidateId);

            if (candidateTestScores.Code == 404)
            {
                return StatusCode(StatusCodes.Status404NotFound, candidateTestScores);
            }

            if (candidateTestScores.Code == 200)
            {
                return StatusCode(StatusCodes.Status200OK, candidateTestScores);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, candidateTestScores);
        }
    }
}
