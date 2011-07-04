using System.Collections.Generic;
using System.Data.Entity;
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

            AddGoalTypes(context, security, privacy);

            AddRiskLevels(context);

            AddTerms(context, security, privacy);

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
        
        private void AddGoalTypes(SquareContext context, SquareType security, SquareType privacy)
        {
            var g1 = new GoalType() {Id = 'S', Name = "Security Goal", IsActive = true, SquareType = security};

            var g2 = new GoalType() {Id = 'P', Name = "Privacy Goal", IsActive = true, SquareType = privacy};
            var g3 = new GoalType() {Id = 'A', Name = "Asset", IsActive = true, SquareType = privacy };

            var g4 = new GoalType() {Id = 'B', Name = "Business Goal", IsActive = true, SquareType = null};

            context.GoalTypes.Add(g1);
            context.GoalTypes.Add(g2);
            context.GoalTypes.Add(g3);
            context.GoalTypes.Add(g4);
        }

        private void AddRiskLevels(SquareContext context)
        {
            var r1 = new RiskLevel()
                         {
                             Id = 'H',
                             Name = "High",
                             SLikelihood = 1.0m,
                             PLikelihood = 3,
                             Impact = 100,
                             Damage = 3,
                             Order = 3,
                             Color = "Red"
                         };

            var r2 = new RiskLevel()
                {
                    Id = 'L',
                    Name = "Low",
                    SLikelihood = 0.1m,
                    PLikelihood = 1,
                    Impact = 10,
                    Damage = 1,
                    Order = 1,
                    Color = "Green"
                };

            var r3 = new RiskLevel()
                {
                    Id = 'M',
                    Name = "Medium",
                    SLikelihood = 0.5m,
                    PLikelihood = 2,
                    Impact = 50,
                    Damage = 2,
                    Order = 2,
                    Color = "Yellow"
                };

            context.RiskLevels.Add(r1);
            context.RiskLevels.Add(r2);
            context.RiskLevels.Add(r3);
        }

        private void AddTerms(SquareContext context, SquareType security, SquareType privacy)
        {
            var terms = new List<Term>();
            var defs = new List<Definition>();

            var t1 = new Term() { Name = "access control", SquareType = security, IsActive = true };
            terms.Add(t1);
            defs.Add(new Definition()
                {
                    Description =
                        "a system which enables an authority to control access to areas and resources in a computer-based information system",
                    Source = "Wikipedia",
                    IsActive = true,
                    Term = t1
                });
            defs.Add(new Definition()
                         {
                             Description = "Limiting access to information system resources only to authorized users, programs, processes, or other systems.",
                             Source = "CNSSI 4009",
                             IsActive = true,
                             Term = t1
                         });

            var t2 = new Term() {Name = "access control list (ACL)", SquareType = security, IsActive = true};
            terms.Add(t2);
            defs.Add(new Definition()
                         {
                             Description = "(ACL) Mechanism implementing discretionary and/or mandatory access control between subjects and objects. ",
                             Source = "CNSSI 4009",
                             IsActive = true,
                             Term = t2
                         });
            defs.Add(new Definition()
            {
                Description = "a list of permissions attached to an object",
                Source = "Wikipedia",
                IsActive = true,
                Term = t2
            });

            var t3 = new Term() { Name = "artifact", SquareType = security, IsActive = true };
            terms.Add(t3);
            defs.Add(new Definition()
            {
                Description = "one of many kinds of tangible by-product produced during the development of software",
                Source = "Wikipedia",
                IsActive = true,
                Term = t3
            });

            var t4 = new Term() { Name = "asset", SquareType = security, IsActive = true };
            terms.Add(t4);
            defs.Add(new Definition()
            {
                Description = "A major application, general support system, high impact program, physical plant, mission critical system, personnel, equipment, or a logically related group of systems.",
                Source = "CNSSI",
                IsActive = true,
                Term = t4
            });


            terms.Add(new Term() { Name = "antivirus software", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "artifact", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "asset", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "attack", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "audit", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "authentication", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "availability", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "back door", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "breach", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "brute force", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "buffer overflow", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "cache cramming", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "cache poisoning", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "confidentiality", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "control", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "corruption", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "cracker", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "denial-of-service (DoS) attack", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "disaster recovery plan", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "disclosure", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "disgruntled employee", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "downtime", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "disruption", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "encryption", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "espionage", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "essential services", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "exposure", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "fabrication", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "fault line attacks", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "fault tolerance", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "firewall", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "hacker", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "honey pot", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "impact", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "incident", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "incident handling", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "insider threat", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "integrity", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "interception", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "interruption", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "intrusion", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "intrusion detection system (IDS)", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "liability", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "luring attack", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "malware", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "man-in-the-middle attack", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "masquerade", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "modification", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "non-essential services", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "non-repudiation", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "patch", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "penetration", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "penetration testing", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "physical security", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "port scanning", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "privacy", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "procedure", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "recognition", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "recovery", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "replay attack", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "resilience", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "resistance", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "risk", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "risk assessment", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "security policy", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "script kiddies", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "spoof", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "stakeholder", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "stealthing", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "survivability", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "target", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "threat", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "threat assessment", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "threat model", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "toolkits", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "Trojan", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "trust", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "uptime", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "victim", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "virus", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "vulnerability", SquareType = security, IsActive = true });
            terms.Add(new Term() { Name = "worm", SquareType = security, IsActive = true });

            foreach (var t in terms)
            {
                context.Terms.Add(t);
            }

            foreach (var d in defs)
            {
                context.Definitions.Add(d);
            }
        }
    }
}