using System;
using System.Collections.Generic;
using System.Data;
using FluentNHibernate;
using FluentNHibernate.Framework;
using NHibernate;
using NHibernate.Cfg;

namespace FluentMapping.Infrastructure.Mappings
{
    public class MySessionSource : ISessionSource
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly Configuration _configuration;

        public MySessionSource(PersistenceModel model) : this(null, model)
        {
        }

        public MySessionSource(IDictionary<string, string> properties, PersistenceModel model)
        {
            if(model == null) throw new ArgumentException("Model cannot be null!");

            _configuration = new Configuration();
            if (properties == null)
                _configuration.Configure();
            else
                _configuration.AddProperties(properties);

            Model = model;
            model.Configure(_configuration);

            _sessionFactory = _configuration.BuildSessionFactory();
        }

        public PersistenceModel Model { get; private set; }

        public ISession CreateSession()
        {
            return _sessionFactory.OpenSession();
        }

        public void BuildSchema()
        {
            var session = CreateSession();
            BuildSchema(session);
        }

        public void BuildSchema(ISession session)
        {
            var connection = session.Connection;

            var drops = _configuration.GenerateDropSchemaScript(_sessionFactory.Dialect);
            executeScripts(drops, connection);

            var scripts = _configuration.GenerateSchemaCreationScript(_sessionFactory.Dialect);
            executeScripts(scripts, connection);
        }

        private static void executeScripts(string[] scripts, IDbConnection connection)
        {
            foreach (var script in scripts)
            {
                var command = connection.CreateCommand();
                command.CommandText = script;
                command.ExecuteNonQuery();
            }
        }
    }
}