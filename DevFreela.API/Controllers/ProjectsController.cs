using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.UpdateProjectCommand;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        public readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public ProjectsController(IProjectService projectService, IMediator mediator)
        {
            _projectService = projectService;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            //var projects = _projectService.Getall(query);

            var queryGetAll = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(queryGetAll);
            return Ok(projects);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateProjectCommand command)
        {

            if(command.Title.Length > 50 )
            {
                return BadRequest();
            }

            //var id = _projectService.Create(inputModel);
            var id = await _mediator.Send(command);

            if(id == 0)
            {
                return NoContent();
            }

            return CreatedAtAction(nameof(GetById), new { id = id} , command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id , [FromBody]UpdateProjectCommand command)
        {
            if (command.Description.Length > 2000)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

           // _projectService.Update(inputModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // _projectService.Delete(id);

            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, [FromBody]CreateCommentCommand command)
        {

            //await _projectService.CreateComment(command);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);

            return NoContent();
        }
    }
}
