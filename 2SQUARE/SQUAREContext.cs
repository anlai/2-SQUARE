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
            // needed for membership to work
            var schema = new aspnet_SchemaVersion()
                             {
                                 Feature = "membership",
                                 CompatibleSchemaVersion = "1",
                                 IsCurrentVersion = true
                             };
            context.AspnetSchemaVersions.Add(schema);

            // square types
            var security = new SquareType() {Name = "Security"};
            var privacy = new SquareType() {Name = "Privacy"};

            context.SquareTypes.Add(security);
            context.SquareTypes.Add(privacy);

            AddSecuritySteps(context, security);
            AddPrivacySteps(context, privacy);

            AddArtifactTypes(context, security, privacy);

            AddAssessmentTypes(context, security, privacy);

            AddElicitationTypes(context, security, privacy);

            context.SaveChanges();
        }

        

        private void AddSecuritySteps(SquareContext context, SquareType security)
        {
            var ss1 = new Step()
                          {
                              Action = "Step1",
                              Controller = "Security",
                              SquareType = security,
                              Description = "",
                              Name = "Agree on Definitions",
                              Order = 1
                          };

            var ss2 = new Step()
                          {
                              Action = "Step2",
                              Controller = "Security",
                              SquareType = security,
                              Description = "",
                              Name = "Identify Security Goals",
                              Order = 2
                          };

            var ss3 = new Step()
            {
                Action = "Step3",
                Controller = "Security",
                SquareType = security,
                Description = "",
                Name = "Develop Artifacts",
                Order = 3
            };

            var ss4 = new Step()
            {
                Action = "Step4",
                Controller = "Security",
                SquareType = security,
                Description = "",
                Name = "Perform Risk Assessment",
                Order = 4
            };

            var ss5 = new Step()
            {
                Action = "Step5",
                Controller = "Security",
                SquareType = security,
                Description = "",
                Name = "Select Elicitation Requirements",
                Order = 5
            };

            var ss6 = new Step()
            {
                Action = "Step6",
                Controller = "Security",
                SquareType = security,
                Description = "",
                Name = "Elicit Security Requirements",
                Order = 6
            };

            var ss7 = new Step()
            {
                Action = "Step7",
                Controller = "Security",
                SquareType = security,
                Description = "",
                Name = "Categorize Requirements",
                Order = 7
            };

            var ss8 = new Step()
            {
                Action = "Step8",
                Controller = "Security",
                SquareType = security,
                Description = "",
                Name = "Prioritize Requirements",
                Order = 8
            };

            var ss9 = new Step()
            {
                Action = "Step9",
                Controller = "Security",
                SquareType = security,
                Description = "",
                Name = "Requirements Inspection",
                Order = 9
            };

            context.Steps.Add(ss1);
            context.Steps.Add(ss2);
            context.Steps.Add(ss3);
            context.Steps.Add(ss4);
            context.Steps.Add(ss5);
            context.Steps.Add(ss6);
            context.Steps.Add(ss7);
            context.Steps.Add(ss8);
            context.Steps.Add(ss9);

        }

        private void AddPrivacySteps(SquareContext context, SquareType privacy)
        {
            var ps1 = new Step()
            {
                Action = "Step1",
                Controller = "Privacy",
                SquareType = privacy,
                Description = "",
                Name = "Agree on Definitions",
                Order = 1
            };

            var ps2 = new Step()
            {
                Action = "Step2",
                Controller = "Privacy",
                SquareType = privacy,
                Description = "",
                Name = "Identify Assets and Privacy Goals",
                Order = 2
            };

            var ps3 = new Step()
            {
                Action = "Step3",
                Controller = "Privacy",
                SquareType = privacy,
                Description = "",
                Name = "Collect Artifacts",
                Order = 3
            };

            var ps4 = new Step()
            {
                Action = "Step4",
                Controller = "Privacy",
                SquareType = privacy,
                Description = "",
                Name = "Risk Assessment",
                Order = 4
            };

            var ps5 = new Step()
            {
                Action = "Step5",
                Controller = "Privacy",
                SquareType = privacy,
                Description = "",
                Name = "Select Elicitation Technique",
                Order = 5
            };

            var ps6 = new Step()
            {
                Action = "Step6",
                Controller = "Privacy",
                SquareType = privacy,
                Description = "",
                Name = "Elicit Privacy Requirements",
                Order = 6
            };

            var ps7 = new Step()
            {
                Action = "Step7",
                Controller = "Privacy",
                SquareType = privacy,
                Description = "",
                Name = "Categorize Requirements",
                Order = 7
            };

            var ps8 = new Step()
            {
                Action = "Step8",
                Controller = "Privacy",
                SquareType = privacy,
                Description = "",
                Name = "Prioritize Requirements",
                Order = 8
            };

            var ps9 = new Step()
            {
                Action = "Step9",
                Controller = "Privacy",
                SquareType = privacy,
                Description = "",
                Name = "Inspect Requirements",
                Order = 9
            };

            context.Steps.Add(ps1);
            context.Steps.Add(ps2);
            context.Steps.Add(ps3);
            context.Steps.Add(ps4);
            context.Steps.Add(ps5);
            context.Steps.Add(ps6);
            context.Steps.Add(ps7);
            context.Steps.Add(ps8);
            context.Steps.Add(ps9);

        }

        private void AddArtifactTypes(SquareContext context, SquareType security, SquareType privacy)
        {
            var at1 = new ArtifactType() {Name = "System Architecture Diagram", SquareType = security};
            var at2 = new ArtifactType() {Name = "Use Case Scenarios", SquareType = security};
            var at3 = new ArtifactType() { Name = "Use Case Diagram", SquareType = security };
            var at4 = new ArtifactType() { Name = "Misuse Case Scenarios", SquareType = security };
            var at5 = new ArtifactType() { Name = "Misuse Case Diagrams", SquareType = security };
            var at6 = new ArtifactType() { Name = "Attack Trees", SquareType = security };
            var at7 = new ArtifactType() { Name = "Standardized Templates and Forms", SquareType = security };

            var at8 = new ArtifactType() { Name = "System Architecture Diagram", SquareType = privacy };
            var at9 = new ArtifactType() { Name = "Use Case Scenarios", SquareType = privacy };
            var at10 = new ArtifactType() { Name = "Use Case Diagram", SquareType = privacy };
            var at11 = new ArtifactType() { Name = "Attack Trees", SquareType = privacy };
            var at12 = new ArtifactType() {Name = "User-Role Hierarchies", SquareType = privacy};

            context.ArtifactTypes.Add(at1);
            context.ArtifactTypes.Add(at2);
            context.ArtifactTypes.Add(at3);
            context.ArtifactTypes.Add(at4);
            context.ArtifactTypes.Add(at5);
            context.ArtifactTypes.Add(at6);
            context.ArtifactTypes.Add(at7);
            context.ArtifactTypes.Add(at8);
            context.ArtifactTypes.Add(at9);
            context.ArtifactTypes.Add(at10);
            context.ArtifactTypes.Add(at11);
            context.ArtifactTypes.Add(at12);
        }

        private void AddAssessmentTypes(SquareContext context, SquareType security, SquareType privacy)
        {
            var at1 = new AssessmentType() { Name = "NIST 800-30", Source = "NIST", SquareType = security, Controller = "NIST800_30" };
            var at2 = new AssessmentType() { Name = "Privacy Risk Analysis for Ubiquitous Computing", Source = "n/a", SquareType = privacy, Controller = "PRAUC" };
            var at3 = new AssessmentType() { Name = "NIST 800-30", Source = "NIST", SquareType = privacy, Controller = "NIST800_30" };

            context.AssessmentTypes.Add(at1);
            context.AssessmentTypes.Add(at2);
            context.AssessmentTypes.Add(at3);
        }

        private void AddElicitationTypes(SquareContext context, SquareType security, SquareType privacy)
        {
            var et1 = new ElicitationType() { Name = "Structured/Unstructured Interviews", SquareType = security, Controller = "GenericElicitation", Description = "" };
            var et2 = new ElicitationType() { Name = "Use/Misuse Cases", SquareType = security, Controller = "GenericElicitation", Description = "" };
            var et3 = new ElicitationType() { Name = "Facilitated Meeting Sessions", SquareType = security, Controller = "GenericElicitation", Description = "Ex. Joint Application Development (JAD) and the Accelerated Requirements Method" };
            var et4 = new ElicitationType() { Name = "Soft Systems Methodology", SquareType = security, Controller = "GenericElicitation", Description = "" };
            var et5 = new ElicitationType() { Name = "Issue-Based Information Systems", SquareType = security, Controller = "GenericElicitation", Description = "" };
            var et6 = new ElicitationType() { Name = "Quality Function Deployment", SquareType = security, Controller = "GenericElicitation", Description = "" };
            var et7 = new ElicitationType() { Name = "Feature-Oriented Domain Analysis", SquareType = security, Controller = "GenericElicitation", Description = "" };
            var et8 = new ElicitationType() { Name = "Controlled Requirements Expression", SquareType = security, Controller = "GenericElicitation", Description = "" };
            var et9 = new ElicitationType() { Name = "Critical Discourse Analysis", SquareType = security, Controller = "GenericElicitation", Description = "" };
            var et10 = new ElicitationType() { Name = "Structured/Unstructured Interviews", SquareType = privacy, Controller = "GenericElicitation", Description = "" };
            var et11 = new ElicitationType() { Name = "Use/Misuse Cases", SquareType = privacy, Controller = "GenericElicitation", Description = "" };
            var et12 = new ElicitationType() { Name = "Facilitate Meeting Sessions", SquareType = privacy, Controller = "GenericElicitation", Description = "Ex. Joint Application Development (JAD) and the Accelerated Requirements Method" };
            var et13 = new ElicitationType() { Name = "Soft Systems Methodology", SquareType = privacy, Controller = "GenericElicitation", Description = "" };
            var et14 = new ElicitationType() { Name = "Issue-Based Information Systems ", SquareType = privacy, Controller = "GenericElicitation", Description = "" };
            var et15 = new ElicitationType() { Name = "Quality Function Deployment", SquareType = privacy, Controller = "GenericElicitation", Description = "" };
            var et16 = new ElicitationType() { Name = "Feature-Oriented Domain Analysis", SquareType = privacy, Controller = "GenericElicitation", Description = "" };
            var et17 = new ElicitationType() { Name = "Privacy Requirements Elicitation Technique (PRET)", SquareType = privacy, Controller = "PRET", Description = "" };

            context.ElicitationTypes.Add(et1);
            context.ElicitationTypes.Add(et2);
            context.ElicitationTypes.Add(et3);
            context.ElicitationTypes.Add(et4);
            context.ElicitationTypes.Add(et5);
            context.ElicitationTypes.Add(et6);
            context.ElicitationTypes.Add(et7);
            context.ElicitationTypes.Add(et8);
            context.ElicitationTypes.Add(et9);
            context.ElicitationTypes.Add(et10);
            context.ElicitationTypes.Add(et11);
            context.ElicitationTypes.Add(et12);
            context.ElicitationTypes.Add(et13);
            context.ElicitationTypes.Add(et14);
            context.ElicitationTypes.Add(et15);
            context.ElicitationTypes.Add(et16);
            context.ElicitationTypes.Add(et17);
        }
    }
}