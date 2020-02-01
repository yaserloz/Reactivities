﻿using MediatR;
using Presistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Delete
    {
         public class Command: IRequest
        {
            public Guid Id { get; set; }
        }
        public class Handler: IRequestHandler<Command>
        {

            public Handler(DataContext context)
            {
                _context = context;
            }

            public DataContext _context { get; }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);
                if (activity == null)
                    throw new Exception("could not find activity");

                _context.Remove(activity);

                var success = await _context.SaveChangesAsync()>0;
                if (success) return Unit.Value;

                throw new Exception("Proplem saving changes");
            }


        }
    }
}
