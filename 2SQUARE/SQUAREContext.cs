using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using _2SQUARE.Core.Aspnet;
using _2SQUARE.Core.Domain;

namespace _2SQUARE
{
    public class SquareContext : DbContext
    {
        // set the membership tables
        public DbSet<aspnet_Application> AspnetApplications { get; set; }
        public DbSet<aspnet_SchemaVersion> AspnetSchemaVersions { get; set; }
        public DbSet<aspnet_User> AspnetUsers { get; set; }

        // set the primary SQUARE tables
        public DbSet<Artifact> Artifacts { get; set; }
        public DbSet<ArtifactType> ArtifactTypes { get; set; }
        public DbSet<AssessmentType> AssessmentTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<ElicitationType> ElicitationTypes { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalType> GoalTypes { get; set; }
        public DbSet<Impact> Impacts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStep> ProjectSteps { get; set; }
        public DbSet<ProjectTerm> ProjectTerms { get; set; }
        public DbSet<ProjectWorker> ProjectWorkers { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<RequirementDefect> RequirementDefects { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<RiskLevel> RiskLevels { get; set; }
        public DbSet<RiskRecommendation> RiskRecommendations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SquareType> SquareTypes { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Term> Terms { get; set; }
    }

    public class SquareInitializer : DropCreateDatabaseAlways<SquareContext>
    {
        // insert the default values needed to operate the tool
        protected override void Seed(SquareContext context)
        {
            base.Seed(context);
        }
    }
}