using ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VotingDbEntity.Repository;

namespace VotingMVCProject.CommonMethod
{
	public static class CandidateRegistration
	{
        private static readonly GetElectionRepository _electionRepository = new GetElectionRepository();
        private static readonly PositionRepository _positionRepository = new PositionRepository();
        public static async Task  Dropdowns(CandidateRegistrationModel model)
        {
            var elections = await _electionRepository.getUpcomingElectionForRegistrationAsync();
            model.Elections = elections
                .Select(e => new SelectListItem
                {
                    Value = e.ElectionId.ToString(),
                    Text = e.Title
                }).ToList();

            var positions = await _positionRepository.getAllPositionsAsync();
            model.Positions = positions
                .Select(e => new SelectListItem
                {
                    Value = e.PositionId.ToString(),
                    Text = e.PositionName
                }).ToList();
        }
    }
}