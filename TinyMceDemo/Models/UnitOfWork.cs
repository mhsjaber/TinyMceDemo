using Proggasoft.Data.Hybrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyMceDemo.Models
{
    public class UnitOfWork : IDisposable
    {
        private ContextClass context;
        private IDbCommandExecutionService dbCommandExecutionService;
        private IDbCommandFactory dbCommandFactory;

        public UnitOfWork(ContextClass context, IDbCommandExecutionService dbCommandExecutionService, IDbCommandFactory dbCommandFactory)
        {
            this.context = context;
            this.dbCommandExecutionService = dbCommandExecutionService;
            this.dbCommandFactory = dbCommandFactory;
        }

        private IGenericRepository<Question> _questionRepository;
        public IGenericRepository<Question> QuestionRepository
        {
            get
            {
                if (this._questionRepository == null)
                {
                    this._questionRepository = new GenericRepository<Question>(context, dbCommandExecutionService, dbCommandFactory);
                }
                return _questionRepository;
            }
        }

        private IGenericRepository<Answer> _answerRepository;
        public IGenericRepository<Answer> AnswerRepository
        {
            get
            {
                if (this._answerRepository == null)
                {
                    this._answerRepository = new GenericRepository<Answer>(context, dbCommandExecutionService, dbCommandFactory);
                }
                return _answerRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}