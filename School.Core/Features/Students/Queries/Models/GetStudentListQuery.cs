using MediatR;
using School.Core.Features.Students.Queries.Results;
using School.Data.Entities;

namespace School.Core.Features.Students.Queries.Models;
public class GetStudentListQuery:IRequest<List<GetStudentListResponse>>
{
}
