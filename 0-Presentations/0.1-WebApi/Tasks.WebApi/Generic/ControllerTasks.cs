using Tasks.Application.Dtos_Genericos;
using Tasks.WebApi.ViewModels.Generic;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Tasks.WebApi.Generic
{
    public abstract class ControllerTasks : Controller
    {
        public readonly IMapper _mapper;

        public ControllerTasks(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected ControllerTasks()
        {
        }

        protected RetornoErrosViewModel TasksResult(BaseDto validationErros)
        {
            var erros = _mapper.Map<List<ValidationFailure>, List<ValidacaoViewModel>>(validationErros.Errors.ToList());

            return new RetornoErrosViewModel { Erros = erros };
        }
    }
}