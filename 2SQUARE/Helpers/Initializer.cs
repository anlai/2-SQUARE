﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Resources;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Core.PRET;
using _2SQUARE.Services;

namespace _2SQUARE.Helpers
{
    public static class Initializer
    {
        // Seeds the database with the necessary values
        public static void Initilize(SquareContext context = null, bool caseStudy = false)
        {
            // if it's null, then it's not being called from the initializer
            if (context == null)
            {
                // instantiate a new square context
                context =  new SquareContext();
                
                // wipe the database
                WipeDatabase(context);
            }

            // square types
            var security = new SquareType() { Name = "Security" };
            var privacy = new SquareType() { Name = "Privacy" };

            context.SquareTypes.Add(security);
            context.SquareTypes.Add(privacy);

            AddSecuritySteps(context, security);
            AddPrivacySteps(context, privacy);

            AddArtifactTypes(context, security, privacy);

            AddAssessmentTypes(context, security, privacy);

            AddElicitationTypes(context, security, privacy);

            AddGoalTypes(context, security, privacy);

            AddRiskLevels(context);

            AddSecurityTerms(context, security);

            AddPrivacyTerms(context, privacy);

            AddProjectRoles(context);

            AddPRET(context);

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw;
            }

            if (caseStudy) InsertCaseStudy(context);
        }

        private static void WipeDatabase(SquareContext context)
        {
            var disableConstraint= "EXEC sp_msforeachtable \"ALTER TABLE ? NOCHECK CONSTRAINT all\"";
            var enableConstraints = "exec sp_msforeachtable \"ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all\"";
            var getTables = "select name from sys.all_objects where type = 'U' and name <> \'EdmMetaData\'";
            var getIdentityTables = "select TABLE_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_SCHEMA = \'dbo\' and COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, \'IsIdentity\') = 1 order by TABLE_NAME";

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SquareContext"].ConnectionString))
            {
                connection.Open();

                var cmd1 = new SqlCommand(disableConstraint, connection);
                var cmd3 = new SqlCommand(enableConstraints, connection);

                cmd1.ExecuteNonQuery();

                var cmd2 = new SqlCommand(getTables, connection);
                var reader = cmd2.ExecuteReader();
                var tables = new List<string>();
                while (reader.Read())
                {
                    tables.Add(reader.GetValue(0).ToString());
                }
                reader.Close();

                foreach (var table in tables)
                {
                    // wipe the tables
                    var cmd = new SqlCommand(string.Format("delete from {0}", table), connection);
                    cmd.ExecuteNonQuery();
                }

                // re-enable the constraints
                cmd3.ExecuteNonQuery();

                // reseed the identities
                tables.Clear();
                var cmd4 = new SqlCommand(getIdentityTables, connection);
                reader = cmd4.ExecuteReader();
                while (reader.Read())
                {
                    tables.Add(reader.GetValue(0).ToString());
                }
                reader.Close();

                foreach (var table in tables)
                {
                    var cmd5 = new SqlCommand(string.Format("DBCC CHECKIDENT ({0}, reseed, 0)", table), connection);
                    cmd5.ExecuteNonQuery();    
                }
                
                connection.Close();
            }
        }

        private static void AddSecuritySteps(SquareContext context, SquareType security)
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

        private static void AddPrivacySteps(SquareContext context, SquareType privacy)
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

        private static void AddArtifactTypes(SquareContext context, SquareType security, SquareType privacy)
        {
            var at1 = new ArtifactType() { Name = "System Architecture Diagram", SquareType = security };
            var at2 = new ArtifactType() { Name = "Use Case Scenarios", SquareType = security };
            var at3 = new ArtifactType() { Name = "Use Case Diagram", SquareType = security };
            var at4 = new ArtifactType() { Name = "Misuse Case Scenarios", SquareType = security };
            var at5 = new ArtifactType() { Name = "Misuse Case Diagrams", SquareType = security };
            var at6 = new ArtifactType() { Name = "Attack Trees", SquareType = security };
            var at7 = new ArtifactType() { Name = "Standardized Templates and Forms", SquareType = security };

            var at8 = new ArtifactType() { Name = "System Architecture Diagram", SquareType = privacy };
            var at9 = new ArtifactType() { Name = "Use Case Scenarios", SquareType = privacy };
            var at10 = new ArtifactType() { Name = "Use Case Diagram", SquareType = privacy };
            var at11 = new ArtifactType() { Name = "Attack Trees", SquareType = privacy };
            var at12 = new ArtifactType() { Name = "User-Role Hierarchies", SquareType = privacy };

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

        private static void AddAssessmentTypes(SquareContext context, SquareType security, SquareType privacy)
        {
            var at1 = new AssessmentType() { Name = "NIST 800-30", Source = "NIST", SquareType = security, Controller = "NIST800_30" };
            var at2 = new AssessmentType() { Name = "Privacy Risk Analysis for Ubiquitous Computing", Source = "n/a", SquareType = privacy, Controller = "PRAUC" };
            var at3 = new AssessmentType() { Name = "NIST 800-30", Source = "NIST", SquareType = privacy, Controller = "NIST800_30" };

            var at4 = new AssessmentType() { Name = "Generic Method", Source = "n/a", Controller = "GenericAssessment", SquareType = security };
            var at5 = new AssessmentType() { Name = "Generic Method", Source = "n/a", Controller = "GenericAssessment", SquareType = privacy };

            context.AssessmentTypes.Add(at1);
            context.AssessmentTypes.Add(at2);
            context.AssessmentTypes.Add(at3);
            context.AssessmentTypes.Add(at4);
            context.AssessmentTypes.Add(at5);
        }

        private static void AddElicitationTypes(SquareContext context, SquareType security, SquareType privacy)
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
            var et18 = new ElicitationType() { Name = "Not Listed", SquareType = security, Controller = "GenericElicitation", Description = "Use for elicitation types not listed." };

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
            context.ElicitationTypes.Add(et18);
        }

        private static void AddGoalTypes(SquareContext context, SquareType security, SquareType privacy)
        {
            var g1 = new GoalType() { Id = "S", Name = "Security Goal", IsActive = true, SquareType = security };

            var g2 = new GoalType() { Id = "P", Name = "Privacy Goal", IsActive = true, SquareType = privacy };
            var g3 = new GoalType() { Id = "A", Name = "Asset", IsActive = true, SquareType = privacy };

            var g4 = new GoalType() { Id = "B", Name = "Business Goal", IsActive = true, SquareType = null };

            context.GoalTypes.Add(g1);
            context.GoalTypes.Add(g2);
            context.GoalTypes.Add(g3);
            context.GoalTypes.Add(g4);
        }

        private static void AddRiskLevels(SquareContext context)
        {
            var r1 = new RiskLevel()
            {
                Id = "H",
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
                Id = "L",
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
                Id = "M",
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

        private static void AddSecurityTerms(SquareContext context, SquareType security)
        {
            var terms = new List<Term>();
            var defs = new List<Definition>();

            var t1 = new Term() { Name = "access control", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Access control ensures that resources are only granted to those users who are entitled to them.", Source = "SANS Institute", IsActive = true, Term = t1 });
            defs.Add(new Definition() { Description = "a system which enables an authority to control access to areas and resources in a computer-based information system", Source = "Wikipedia", IsActive = true, Term = t1 });
            defs.Add(new Definition() { Description = "Limiting access to information system resources only to authorized users, programs, processes, or other systems.", Source = "CNSSI 4009", IsActive = true, Term = t1 });
            var t2 = new Term() { Name = "access control list", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A table that tells a computer operating system which access rights or explicit denials each user has to a particular system object, such as a file directory or individual file [TechTarget 05].", Source = "http://watis.techtarget.com", IsActive = true, Term = t2 });
            defs.Add(new Definition() { Description = "(ACL) Mechanism implementing discretionary and/or mandatory access control between subjects and objects. ", Source = "CNSSI 4009", IsActive = true, Term = t2 });
            defs.Add(new Definition() { Description = "a list of permissions attached to an object", Source = "Wikipedia", IsActive = true, Term = t2 });
            var t3 = new Term() { Name = "antivirus software", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A program that searches hard drives and floppy disks for any known or potential viruses [TechTarget 05].", Source = "http://watis.techtarget.com", IsActive = true, Term = t3 });
            var t4 = new Term() { Name = "artifact", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The remnants of an intruder attack or incident activity. These could be software used by intruder(s), a collection of tools, malicious code, logs, files, output from tools, or the status of a system after an attack or intrusion [West-Brown 03].", Source = "Handbook for Computer Security Incident Response Teams", IsActive = true, Term = t4 });
            defs.Add(new Definition() { Description = "one of many kinds of tangible by-product produced during the development of software", Source = "Wikipedia", IsActive = true, Term = t4 });
            var t5 = new Term() { Name = "asset", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A critical valuable that a company owns and wants to secure.", Source = "SQUARE Case Study", IsActive = true, Term = t5 });
            defs.Add(new Definition() { Description = "A major application, general support system, high impact program, physical plant, mission critical system, personnel, equipment, or a logically related group of systems.", Source = "CNSSI", IsActive = true, Term = t5 });
            var t6 = new Term() { Name = "attack", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "An action conducted by an adversary, the attacker, on a potential victim. A set of events that an observer believes to have information assurance consequences on some entity, the target of the attack [Ellison 03].", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", IsActive = true, Term = t6 });
            var t7 = new Term() { Name = "auditing", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The information gathering and analysis of assets to ensure such things as policy com- pliance and security from vulnerabilities [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t7 });
            var t8 = new Term() { Name = "authentication", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The process of determining whether someone or something is, in fact, who or what it is declared to be [TechTarget 05].", Source = "http://watis.techtarget.com", IsActive = true, Term = t8 });
            var t9 = new Term() { Name = "availability", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The property of a system or a system resource being accessible and usable upon de- mand by an authorized system entity, according to performance specifications for the system; i.e., a system is available if it provides services according to the system de- sign whenever users request them [Allen 99].", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", IsActive = true, Term = t9 });
            var t10 = new Term() { Name = "back door", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "An element in a system that allows access by bypassing access controls [Howard 97].", Source = "http://www.cert.org/research/JHThesis/Start.html", IsActive = true, Term = t10 });
            var t11 = new Term() { Name = "breach", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Any intentional event in which an intruder gains access that compromises the confi- dentiality, integrity, or availability of computers, networks, or the data residing on them [CERT/CC 05].", Source = "http://www.cert.org/security-improvement/modules/m06.html", IsActive = true, Term = t11 });
            var t12 = new Term() { Name = "brute force", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A cryptanalysis technique or other kind of attack method involving an exhaustive procedure that tries all possibilities, one by one [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t12 });
            var t13 = new Term() { Name = "buffer overflow", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A buffer overflow occurs when a program or process tries to store more data in a buffer (temporary data storage area) than it was intended to hold. Since buffers are created to contain a finite amount of data, the extra information— which has to go somewhere—can overflow into adjacent buffers, corrupting or overwriting the valid data held in them [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t13 });
            var t14 = new Term() { Name = "cache cramming", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The technique of tricking a browser to run cached Java code from the local disk in- stead of the Internet zone, so it runs with less restrictive permissions [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t14 });
            var t15 = new Term() { Name = "cache poisoning", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Malicious or misleading data from a remote name server is saved [cached] by another name server. Typically used with Domain Name System (DNS) cache poisoning attacks [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t15 });
            var t16 = new Term() { Name = "confidentiality", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The property that information is not made available or disclosed to unauthorized individuals, entities, or processes (i.e., to any unauthorized system entity) [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t16 });
            var t17 = new Term() { Name = "control", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "An action, device, procedure, or technique that removes or reduces a vulnerability", Source = "SQUARE Case Study", IsActive = true, Term = t17 });
            var t18 = new Term() { Name = "corruption", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A threat action that undesirably alters system operation by adversely modifying sys- tem functions or data [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t18 });
            var t19 = new Term() { Name = "cracker", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Someone who breaks into someone else’s computer system, often on a network; by- passes passwords or licenses in computer programs; or in other ways intentionally breaches computer security [TechTarget 05].", Source = "http://watis.techtarget.com", IsActive = true, Term = t19 });
            var t20 = new Term() { Name = "denial-of-service (DoS) attack", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A form of attacking another computer or company by sending millions of requests every second, causing the network to slow down, cause errors, or shut down.", Source = "Computer Hope", IsActive = true, Term = t20 });
            var t21 = new Term() { Name = "disaster recovery plan", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A disaster recovery plan (DRP)—sometimes referred to as a business continuity plan (BCP) or business process contingency plan (BPCP)—describes how an organization is to deal with potential disasters [TechTarget 05].", Source = "http://watis.techtarget.com", IsActive = true, Term = t21 });
            var t22 = new Term() { Name = "disclosure", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The dissemination of information to anyone who is not authorized to access that in- formation [Alberts 03].", Source = "http://www.cert.org/archive/pdf/OCTAVEthreatProfiles.pdf", IsActive = true, Term = t22 });
            var t23 = new Term() { Name = "disgruntled employee", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A person in an organization who deliberately abuses or misuses computer systems and their information [Alberts 03].", Source = "http://www.cert.org/archive/pdf/OCTAVEthreatProfiles.pdf", IsActive = true, Term = t23 });
            var t24 = new Term() { Name = "downtime", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The amount of time a system is down in a given period. This will include crashes and system problems as well as scheduled maintenance work [RUsecure 05].", Source = "http://www.yourwindow.to/information-security", IsActive = true, Term = t24 });
            var t25 = new Term() { Name = "disruption", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A circumstance or event that interrupts or prevents the correct operation of system services and functions [Alberts 03].", Source = "http://www.cert.org/archive/pdf/OCTAVEthreatProfiles.pdf", IsActive = true, Term = t25 });
            var t26 = new Term() { Name = "encryption", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Cryptographic transformation of data (called “plaintext”) into a form (called “cipher text”) that conceals the data’s original meaning to prevent it from being known or used [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t26 });
            var t27 = new Term() { Name = "espionage", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The act or practice of spying or of using spies to obtain secret information about another government or a business competitor [Dictionary.com 05].", Source = "http://dictionary.reference.com", IsActive = true, Term = t27 });
            var t28 = new Term() { Name = "essential services", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Services to users of a system that must be provi ded even in the presence of intrusion, failure, or accident [Ellison 97].", Source = "http://www.sei.cmu.edu/publications/documents/97.reports/97tr013/97tr013abstract.html", IsActive = true, Term = t28 });
            var t29 = new Term() { Name = "exposure", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The dissemination of information to anyone who is not authorized to access that in- formation [Alberts 03].", Source = "http://www.cert.org/archive/pdf/OCTAVEthreatProfiles.pdf", IsActive = true, Term = t29 });
            var t30 = new Term() { Name = "fabrication", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Aims to fool other machines on the network into accepting the imposter as an origi- nal, either to lure the other machines into sending it data or to allow it to alter data [Howard 98].", Source = "http://www.cert.org/research/taxonomy_988667.pdf", IsActive = true, Term = t30 });
            var t31 = new Term() { Name = "fault line attacks", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Fault line attacks use weaknesses between interfaces of systems to exploit gaps in coverage [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t31 });
            var t32 = new Term() { Name = "fault tolerance", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Describes a computer system or component designed so that, in the event that a com- ponent fails, a backup component or procedure can immediately take its place with no loss of service. Fault tolerance can be provided with software, or embedded in hardware, or provided by some combination [TechTarget 05].", Source = "http://watis.techtarget.com", IsActive = true, Term = t32 });
            var t33 = new Term() { Name = "firewall", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A system designed to prevent unauthorized access to or from a private network. Fire- walls can be implemented in both hardware and software, or a combination of both [Webopedia 05].", Source = "http://www.webopedia.com", IsActive = true, Term = t33 });
            var t34 = new Term() { Name = "hacker", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "An individual who breaks into computers primarily for the challenge and status of obtaining access [Howard 97].", Source = "http://www.cert.org/research/JHThesis/Start.html", IsActive = true, Term = t34 });
            var t35 = new Term() { Name = "honey pot", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Programs that simulate one or more network services designated on a computer’s ports. An attacker assumes that vulnerable services that can be used to break into the machine are being run. A honey pot can be used to log access attempts to those ports, including the attacker’s keystrokes. This can provide advanced warning of a more concerted attack [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t35 });
            var t36 = new Term() { Name = "HTTP header manipulation", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "HTTP requests and responses send information in the HTTP headers. HTTP headers are a series of lines containing a name/value pair used to pass information such as the host, referrer, user agent, etc. HTTP headers can be manipulated to cause SQL injec- tion or cross-site scripting errors.", Source = "ASI", IsActive = true, Term = t36 });
            var t37 = new Term() { Name = "impact", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The negative effect of an attack on a victim system by an attacker [Allen 99].", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", IsActive = true, Term = t37 });
            var t38 = new Term() { Name = "incident", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "An adverse network event in an information system or network or the threat of the occurrence of such an event [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t38 });
            var t39 = new Term() { Name = "incident handling", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "An action plan for dealing with intrusions, cyber theft, denial of service, fire, floods, and other security-related events [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t39 });
            var t40 = new Term() { Name = "insider threat", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The threat that authorized personnel of an organization will act counter to the organi- zation’s security and interest, especially for the purposes of sabotage and espionage [NIPC 02].", Source = "http://www.hpcc-usa.org/pics/02-pres/wright.ppt", IsActive = true, Term = t40 });
            var t41 = new Term() { Name = "integrity", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "For systems, the quality that a system has when it can perform its intended function in a unimpaired manner, free from deliberate or inadvertent unauthorized manipula- tion. For data, the property that data has not been changed, destroyed, or lost in an unauthorized or accidental manner [Allen 99].", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", IsActive = true, Term = t41 });
            var t42 = new Term() { Name = "interception", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Access to an asset gained by an unauthorized party [Pfleeger 03].", Source = "Security in Computing", IsActive = true, Term = t42 });
            var t43 = new Term() { Name = "interruption", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "An event that causes an asset of a system to be destroyed or become unavailable or unusable [Howard 97].", Source = "http://www.cert.org/research/JHThesis/Start.html", IsActive = true, Term = t43 });
            var t44 = new Term() { Name = "intrusion", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "An attack on a network for the purpose of gaining access to or destroying privileged information or disrupting services to legitimate users [Ellison 03].", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", IsActive = true, Term = t44 });
            var t45 = new Term() { Name = "intrusion detection system", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A combination of hardware and software that monitors and collects system and net- work information and analyzes it to determine if an attack or an intrusion has oc- curred. Some ID systems can automatically respond to an intrusion [Allen 99].", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", IsActive = true, Term = t45 });
            var t46 = new Term() { Name = "intrusion prevention system", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A system used to actively drop packets of data or disconnect connections that contain unauthorized data. Intrusion prevention technology is also commonly an extension of intrusion detection technology [Wiki 05].", Source = "www.wikipedia.org", IsActive = true, Term = t46 });
            var t47 = new Term() { Name = "liability", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The responsibility of someone for damage or loss [West-Brown 03].", Source = "http://www.sei.cmu.edu/publications/documents/03.reports/03hb002.html", IsActive = true, Term = t47 });
            var t48 = new Term() { Name = "luring attack", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A type of elevation of privilege attack where the attacker “lures” a more highly privi- leged component to do something on his or her behalf. The most straightforward technique is to convince the target to run the attacker’s code in a more privileged security context [Brown 05].", Source = "“Item 7: What is a Luring Attack?” The .NET Developer’s Guide to Windows Security", IsActive = true, Term = t48 });
            var t49 = new Term() { Name = "malware", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Programming or files that are developed for the purpose of doing harm. Thus, mal- ware includes computer viruses, worms, and Trojan horses [Webopedia 05].", Source = "http://www.webopedia.com", IsActive = true, Term = t49 });
            var t50 = new Term() { Name = "man-in-the-middle attack", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "An attack in which the attacker is able to read, and possibly modify at will, messages between two parties without letting either party know that they have been attacked. The attacker must be able to observe and intercept messages going between the two victims [Farlex 05].", Source = "http://encyclopedia.thefreedictionary.com/man%20in%20the%20middle%20attack", IsActive = true, Term = t50 });
            var t51 = new Term() { Name = "masquerade", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Aims to fool other machines on the network into accepting the imposter as an origi- nal, either to lure the other machines into sending it data or to allow it to alter data [Howard 98].", Source = "http://www.cert.org/research/taxonomy_988667.pdf", IsActive = true, Term = t51 });
            var t52 = new Term() { Name = "modification", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Situation in which an unauthorized party not only gains access to, but tampers with an asset [Howard 97].", Source = "http://www.cert.org/research/JHThesis/Start.html", IsActive = true, Term = t52 });
            var t53 = new Term() { Name = "non-essential services", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Services to users of a system that can be temporarily suspended to permit delivery of essential services while the system is dealing with intrusions and compromises [Elli- son 97].", Source = "http://www.sei.cmu.edu/publications/documents/97.reports/97tr013/97tr013abstract.html", IsActive = true, Term = t53 });
            var t54 = new Term() { Name = "non-repudiation", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The goal of non-repudiation is to prove that a message has been sent and received [SSI 05].", Source = "http://www.ssimail.com/Glossary.htm#N", IsActive = true, Term = t54 });
            var t55 = new Term() { Name = "patch", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A small update released by a software manufacturer to fix bugs in an existing pro- gram [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t55 });
            var t56 = new Term() { Name = "patching", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The process of updating software to a new version that fixes bugs in a previous ver- sion [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t56 });
            var t57 = new Term() { Name = "penetration", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Intrusion, trespassing, or unauthorized entry into a system [RUsecure 05].", Source = "http://www.yourwindow.to/information-security", IsActive = true, Term = t57 });
            var t58 = new Term() { Name = "penetration testing", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The execution of a testing plan, the sole purpose of which is to attempt to hack into a system using known tools and techniques [RUsecure 05].", Source = "http://www.yourwindow.to/information-security", IsActive = true, Term = t58 });
            var t59 = new Term() { Name = "physical security", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Security measures taken to protect systems, buildings, and related supporting infra- structure against threats associated with their physical environment [Guttman 95].", Source = "http://encyclopedia.thefreedictionary.com/man%20in%20the%20middle%20attack", IsActive = true, Term = t59 });
            var t60 = new Term() { Name = "port scanning", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The act of systematically scanning a computer’s ports [Webopedia 05].", Source = "http://www.webopedia.com", IsActive = true, Term = t60 });
            var t61 = new Term() { Name = "privacy", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The quality or condition of being secluded from the presence or view of others [Dic- tionary.com 05].", Source = "http://dictionary.reference.com", IsActive = true, Term = t61 });
            var t62 = new Term() { Name = "procedure", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The implementation of a policy in the forms of workflows, orders, or mechanisms [West-Brown 03].", Source = "http://www.sei.cmu.edu/publications/documents/03.reports/03hb002.html", IsActive = true, Term = t62 });
            var t63 = new Term() { Name = "recognition", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The capability of a system to recognize attacks or the probing that precedes attacks [Ellison 03].", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", IsActive = true, Term = t63 });
            var t64 = new Term() { Name = "recovery", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A system’s ability to restore services after an intrusion has occurred. Recovery also contributes to a system’s ability to maintain essential services during intrusion [Elli- son 03].", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", IsActive = true, Term = t64 });
            var t65 = new Term() { Name = "replay attack", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The interception of communications, such as an authentication communication, and subsequent impersonation of the sender by retransmitting the intercepted communi- cation [FFIEC 04].", Source = "http://www.ffiec.gov/ffiecinfobase/booklets/information_security/08_glossary.html", IsActive = true, Term = t65 });
            var t66 = new Term() { Name = "resilience", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The ability of a computer or system to both withstand a range of load fluctuations and also remain stable under continuous and/or adverse conditions [RUsecure 05].", Source = "http://www.yourwindow.to/information-security", IsActive = true, Term = t66 });
            var t67 = new Term() { Name = "resistance", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Capability of a system to resist attacks [Ellison 03].", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", IsActive = true, Term = t67 });
            var t68 = new Term() { Name = "risk", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The product of the level of threat with the level of vulnerability. It establishes the likelihood of a successful attack [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t68 });
            var t69 = new Term() { Name = "risk assessment", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The process by which risks are identified and the impact of those risks determined [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t69 });
            var t70 = new Term() { Name = "security policy", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A policy that addresses security issues [West-Brown 03].", Source = "http://www.sei.cmu.edu/publications/documents/03.reports/03hb002.html", IsActive = true, Term = t70 });
            var t71 = new Term() { Name = "script kiddies", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The more immature but unfortunately often just as dangerous exploiter of security lapses on the Internet. The typical script kiddy uses existing and frequently well known and easy-to-find techniques and programs or scripts to search for and exploit weaknesses in other computers on the Internet—often randomly and with little regard or perhaps even understanding of the potentially harmful consequences [TechTarget 05].", Source = "http://watis.techtarget.com", IsActive = true, Term = t71 });
            var t72 = new Term() { Name = "spoof", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The term is used to describe a variety of ways in which hardware and software can be fooled. IP spoofing, for example, involves trickery that makes a message appear as if it came from an authorized IP address [Webopedia 04].", Source = "http://www.webopedia.com", IsActive = true, Term = t72 });
            var t73 = new Term() { Name = "SQL injection", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A type of input validation attack specific to database-driven applications where SQL code is inserted into application queries to manipulate the database [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t73 });
            var t74 = new Term() { Name = "stakeholder", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Anyone who is a direct user, indirect user, manager of users, senior manager, opera- tions staff member, support (help desk) staff member, developer working on other systems that integrate or interact with the one under development, or maintenance professionals potentially affected by the development and/or deployment of a soft- ware project [Ambler 04].", Source = "http://www.agilemodeling.com/essays/activeStakeholderParticipation.htm", IsActive = true, Term = t74 });
            var t75 = new Term() { Name = "stealthing", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A term that refers to approaches used by malicious code to conceal its presence on an infected system [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t75 });
            var t76 = new Term() { Name = "survivability", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The capability of a system to complete its mission in a timely manner, even if sig- nificant portions are compromised by attack or accident. The system should provide essential services in the presence of successful intrusion and recover compromised services in a timely manner after intrusion occurs [Mead 03].", Source = "http://www.sei.cmu.edu/publications/documents/03.reports/03tn013.html", IsActive = true, Term = t76 });
            var t77 = new Term() { Name = "target", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The object of an attack, especially host, computer, network, system, site, person, organization, nation, company, government, or other group [Allen 99].", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", IsActive = true, Term = t77 });
            var t78 = new Term() { Name = "threat", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A potential for violation of security, which exists when there is a circumstance, capa- bility, action, or event that could breach security and cause harm [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t78 });
            var t79 = new Term() { Name = "threat assessment", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The identification of the types of threats that an organization might be exposed to [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t79 });
            var t80 = new Term() { Name = "threat model", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Used to describe a given threat and the harm it could to do a system if it has a vulner- ability [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t80 });
            var t81 = new Term() { Name = "toolkits", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A collection of tools with related purposes or functions, e.g., antivirus toolkit, disk toolkit [RUsecure 05].", Source = "http://www.yourwindow.to/information-security", IsActive = true, Term = t81 });
            var t82 = new Term() { Name = "Trojan", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A program in which malicious or harmful code is contained inside apparently harm- less programming or data in such a way that it can get control and do its chosen form of damage, such as ruining the file allocation table on a hard disk [TechTarget 05].", Source = "http://watis.techtarget.com", IsActive = true, Term = t82 });
            var t83 = new Term() { Name = "trust", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "Determines which permissions other systems or users have and what actions they can perform on remote machines [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t83 });
            var t84 = new Term() { Name = "uptime", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "The property of a system or a system resource being accessible and usable upon de- mand by an authorized system entity, according to performance specifications for the system; i.e., a system is available if it provides services according to the system de- sign whenever users request them [Allen 99].", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", IsActive = true, Term = t84 });
            var t85 = new Term() { Name = "victim", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "That which is the target of an attack. An entity may be a victim of either a successful or unsuccessful attack [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t85 });
            var t86 = new Term() { Name = "virus", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A hidden, self-replicating section of computer software, usually malicious logic, that propagates by infecting—i.e., inserting a copy of itself into and becoming part of— another program. A virus cannot run by itself; it requires that its host program be run to make it active [SANS 05].", Source = "http://www.sans.org/resources/glossary.php", IsActive = true, Term = t86 });
            var t87 = new Term() { Name = "vulnerability", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A condition or weakness in (or absence of) security procedures, technical controls, physical controls, or other controls that could be exploited by a threat [Guttman 95].", Source = "http://csrc.nist.gov/publications/nistpubs/800-12/handbook.pdf", IsActive = true, Term = t87 });
            var t88 = new Term() { Name = "worm", SquareType = security, IsActive = true };
            defs.Add(new Definition() { Description = "A self-replicating virus that does not alter files but resides in active memory and duplicates itself. Worms use parts of an operating system that are automatic and usu- ally invisible to the user. It is common for worms to be noticed only when their un- controlled replication consumes system resources, slowing or halting other tasks [TechTarget 05].", Source = "http://watis.techtarget.com", IsActive = true, Term = t88 });

            // strip out the [reference], since data was taken from case study
            foreach (var def in defs)
            {
                var index = def.Description.IndexOf('[');

                def.Description = def.Description.Substring(0, index < 0 ? def.Description.Length : index);
            }

            foreach (var t in terms)
            {
                context.Terms.Add(t);
            }

            foreach (var d in defs)
            {
                context.Definitions.Add(d);
            }
        }

        private static void AddPrivacyTerms(SquareContext context, SquareType privacy)
        {
            var terms = new List<Term>();
            var defs = new List<Definition>();

            terms.Add(new Term() { Name = "access", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "aggregation", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "anonymity", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "anonymous", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "application of denial of service", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "application modification", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "appropriation", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "authentication", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "authorization", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "blackmail", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "client-side profiles", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "contact", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "confidentiality", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "cookie", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "credential theft", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "data breach", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "data controller", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "data exposure", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "data privacy", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "data quality", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "disclosure", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "distortion", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "exclusion", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "exposure", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "fair information practice", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "functional manipulation", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "identification", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "identity fraud", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "increased accessibility", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "information aggregation", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "information collection", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "information monitoring", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "information personaliza-tion", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "information storage", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "information transfer", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "insecurity", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "interrogation", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "network credential theft", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "network denial of service", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "network exposure", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "openness", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "privacy", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "privacy act", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "privacy policy", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "privacy protection", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "right to privacy", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "pseudonymity", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "pseudonymous profile", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "secondary use", SquareType = privacy, IsActive = true });
            terms.Add(new Term() { Name = "surveillance", SquareType = privacy, IsActive = true });

            foreach (var t in terms)
            {
                context.Terms.Add(t);
            }

            foreach (var d in defs)
            {
                context.Definitions.Add(d);
            }
        }

        private static void AddProjectRoles(SquareContext context)
        {
            context.ProjectRoles.Add(new ProjectRole() { Id = "PM", Name = "Project Manager" });
            context.ProjectRoles.Add(new ProjectRole() { Id = "RE", Name = "Requirements Engineer" });
            context.ProjectRoles.Add(new ProjectRole() { Id = "SH", Name = "Stakeholder" });
        }

        private static void AddPRET(SquareContext context)
        {
            var q1 = new PRETQuestion()
            {
                Question = "Does the service provider process personal information?",
                Order = 1,
                SubQuestion = false
            };

            context.PretQuestions.Add(q1);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Yes", Question = q1 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "No", Question = q1 });

            var q2 = new PRETQuestion()
            {
                Question = "In which country or area is the service provided?",
                Order = 2,
                SubQuestion = false
            };

            context.PretQuestions.Add(q2);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "USA", Question = q2 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "EU", Question = q2 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Canada", Question = q2 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Japan", Question = q2 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Other", Question = q2 });

            var q3 = new PRETQuestion()
            {
                Question = "What type of service provider?",
                Order = 3,
                SubQuestion = false
            };

            context.PretQuestions.Add(q3);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Indistrial", Question = q3 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Governmental", Question = q3 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Academic", Question = q3 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Other", Question = q3 });

            var q4 = new PRETQuestion()
            {
                Question = "If Industrial, does the service provider belong to any of these fields?",
                Order = 1,
                SubQuestion = true,
                ParentQuestion = q3
            };

            context.PretQuestions.Add(q4);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Medicine", Question = q4 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Communication", Question = q4 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Education", Question = q4 });

            var q5 = new PRETQuestion()
            {
                Question = "If Governmental, does the service provider belong to any of these fields",
                Order = 2,
                SubQuestion = true,
                ParentQuestion = q3
            };

            context.PretQuestions.Add(q5);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Military Branch", Question = q5 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Nonmilitary Branch", Question = q5 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Research Body", Question = q5 });

            var q6 = new PRETQuestion()
            {
                Question = "Is the purpose of the service related to journalism, literary work, academic studies, religious activities, or political activities?",
                Order = 3,
                SubQuestion = true,
                ParentQuestion = q3
            };

            context.PretQuestions.Add(q6);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Yes", Question = q6 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "No", Question = q6 });

            var q7 = new PRETQuestion()
            {
                Question = "What kind of personal information does the service provider process?",
                Order = 4,
                SubQuestion = false
            };

            context.PretQuestions.Add(q7);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Point of Contact", Question = q7 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Social Identification", Question = q7 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Personal Identity Data", Question = q7 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Demographic Information", Question = q7 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Age, Education", Question = q7 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Health Information", Question = q7 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Financial Information", Question = q7 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Personal Information of Children", Question = q7 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Other Sensitive Personal Information", Question = q7 });

            var q8 = new PRETQuestion()
            {
                Question = "How does the service provider obtain personal information?",
                Order = 5,
                SubQuestion = false
            };

            context.PretQuestions.Add(q8);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Provided by Users", Question = q8 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Provided by Third Parties", Question = q8 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Collected Automatically from Users", Question = q8 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Collected Automatically from Thirs Parties", Question = q8 });

            var q9 = new PRETQuestion()
            {
                Question = "Where does the service provider store personal information",
                Order = 6,
                SubQuestion = false
            };

            context.PretQuestions.Add(q9);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Client Side", Question = q9 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Server Side", Question = q9 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Third Party Client", Question = q9 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Third PArty Server Side", Question = q9 });

            var q10 = new PRETQuestion()
            {
                Question = "How long does the service provider store personal information?",
                Order = 7,
                SubQuestion = false
            };

            context.PretQuestions.Add(q10);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Does Not Store", Question = q10 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "One Transaction", Question = q10 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Certain Period of Time", Question = q10 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Forever", Question = q10 });

            var q11 = new PRETQuestion()
            {
                Question = "Does the service provider use personal information for another purpose?",
                Order = 8,
                SubQuestion = false
            };

            context.PretQuestions.Add(q11);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Yes", Question = q11 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "No", Question = q11 });

            var q12 = new PRETQuestion()
            {
                Question = "Does the service provider share personal information with others?",
                Order = 9,
                SubQuestion = false
            };

            context.PretQuestions.Add(q12);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "Yes", Question = q12 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "No", Question = q12 });

            var q13 = new PRETQuestion()
            {
                Question = "What privacy protection level does the service provider set?",
                Order = 10,
                SubQuestion = false
            };

            context.PretQuestions.Add(q13);

            context.PretAnswers.Add(new PRETAnswer() { Answer = "High", Question = q13 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Mid", Question = q13 });
            context.PretAnswers.Add(new PRETAnswer() { Answer = "Low", Question = q13 });
        }

        /// <summary>
        ///  Inserts the values available for the case study
        /// </summary>
        /// <param name="context"></param>
        private static void InsertCaseStudy(SquareContext context)
        {
            var projectService = new ProjectsService();

            var token = CodeFirstMembershipDemoSharp.Code.CodeFirstSecurity.CreateAccount("casestudy", "password", "alan.n.lai@gmail.com");
            var user = context.Users.Where(a => a.Username == "casestudy").Single();

            // create the project
            var steps = context.Steps;
            var squareTypes = context.SquareTypes;
            var security = squareTypes.Where(a => a.Name == SquareTypes.Security).Single();

            var project = new Project() { Name = "Asset Management System", Description = "IT Management system, developed for the Acme Company as part of a case study for the SQUARE process.  More details can be obtained at the following addresses. (http://www.sei.cmu.edu/library/abstracts/reports/04sr015.cfm) (http://www.sei.cmu.edu/library/abstracts/reports/05sr005.cfm) (http://www.sei.cmu.edu/library/abstracts/reports/06sr003.cfm)" };

            // fill in all the project steps
            foreach (var squareTypeId in squareTypes.Select(a => a.Id).ToList())
            {
                foreach (var step in steps.Where(a => a.SquareType.Id == squareTypeId))
                {
                    var pstep = new ProjectStep() { Project = project, Step = step, Complete = (squareTypeId == security.Id), DateStarted = (squareTypeId == security.Id) ? (DateTime?)DateTime.Now : null, DateCompleted = (squareTypeId == security.Id) ? (DateTime?)DateTime.Now : null };
                    project.ProjectSteps.Add(pstep);
                }
            }

            // assign the roles
            var role = context.ProjectRoles.Where(a => a.Id == ProjectRoles.ProjectManager).Single();            
            var worker = new ProjectWorker() { Project = project, Role = role, User = user };
            project.ProjectWorkers.Add(worker);

            // create the terms
            #region Project Terms
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "access control", Definition = "Access control ensures that resources are only granted to those users who are entitled to them.", Source = "SANS Institute", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "access control list", Definition = "A table that tells a computer operating system which access rights or explicit denials each user has to a particular system object, such as a file directory or individual file ", Source = "http://watis.techtarget.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "antivirus software", Definition = "A program that searches hard drives and floppy disks for any known or potential viruses ", Source = "http://watis.techtarget.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "artifact", Definition = "The remnants of an intruder attack or incident activity. These could be software used by intruder(s), a collection of tools, malicious code, logs, files, output from tools, or the status of a system after an attack or intrusion ", Source = "Handbook for Computer Security Incident Response Teams", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "asset", Definition = "A critical valuable that a company owns and wants to secure.", Source = "SQUARE Case Study", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "attack", Definition = "An action conducted by an adversary, the attacker, on a potential victim. A set of events that an observer believes to have information assurance consequences on some entity, the target of the attack ", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "auditing", Definition = "The information gathering and analysis of assets to ensure such things as policy com- pliance and security from vulnerabilities ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "authentication", Definition = "The process of determining whether someone or something is, in fact, who or what it is declared to be ", Source = "http://watis.techtarget.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "availability", Definition = "The property of a system or a system resource being accessible and usable upon de- mand by an authorized system entity, according to performance specifications for the system; i.e., a system is available if it provides services according to the system de- sign whenever users request them ", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "back door", Definition = "An element in a system that allows access by bypassing access controls ", Source = "http://www.cert.org/research/JHThesis/Start.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "breach", Definition = "Any intentional event in which an intruder gains access that compromises the confi- dentiality, integrity, or availability of computers, networks, or the data residing on them ", Source = "http://www.cert.org/security-improvement/modules/m06.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "brute force", Definition = "A cryptanalysis technique or other kind of attack method involving an exhaustive procedure that tries all possibilities, one by one ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "buffer overflow", Definition = "A buffer overflow occurs when a program or process tries to store more data in a buffer (temporary data storage area) than it was intended to hold. Since buffers are created to contain a finite amount of data, the extra information— which has to go somewhere—can overflow into adjacent buffers, corrupting or overwriting the valid data held in them ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "cache cramming", Definition = "The technique of tricking a browser to run cached Java code from the local disk in- stead of the Internet zone, so it runs with less restrictive permissions ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "cache poisoning", Definition = "Malicious or misleading data from a remote name server is saved ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "confidentiality", Definition = "The property that information is not made available or disclosed to unauthorized individuals, entities, or processes (i.e., to any unauthorized system entity) ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "control", Definition = "An action, device, procedure, or technique that removes or reduces a vulnerability", Source = "SQUARE Case Study", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "corruption", Definition = "A threat action that undesirably alters system operation by adversely modifying sys- tem functions or data ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "cracker", Definition = "Someone who breaks into someone else’s computer system, often on a network; by- passes passwords or licenses in computer programs; or in other ways intentionally breaches computer security ", Source = "http://watis.techtarget.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "denial-of-service (DoS) attack", Definition = "A form of attacking another computer or company by sending millions of requests every second, causing the network to slow down, cause errors, or shut down.", Source = "Computer Hope", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "disaster recovery plan", Definition = "A disaster recovery plan (DRP)—sometimes referred to as a business continuity plan (BCP) or business process contingency plan (BPCP)—describes how an organization is to deal with potential disasters ", Source = "http://watis.techtarget.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "disclosure", Definition = "The dissemination of information to anyone who is not authorized to access that in- formation ", Source = "http://www.cert.org/archive/pdf/OCTAVEthreatProfiles.pdf", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "disgruntled employee", Definition = "A person in an organization who deliberately abuses or misuses computer systems and their information ", Source = "http://www.cert.org/archive/pdf/OCTAVEthreatProfiles.pdf", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "downtime", Definition = "The amount of time a system is down in a given period. This will include crashes and system problems as well as scheduled maintenance work ", Source = "http://www.yourwindow.to/information-security", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "disruption", Definition = "A circumstance or event that interrupts or prevents the correct operation of system services and functions ", Source = "http://www.cert.org/archive/pdf/OCTAVEthreatProfiles.pdf", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "encryption", Definition = "Cryptographic transformation of data (called “plaintext”) into a form (called “cipher text”) that conceals the data’s original meaning to prevent it from being known or used ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "espionage", Definition = "The act or practice of spying or of using spies to obtain secret information about another government or a business competitor ", Source = "http://dictionary.reference.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "essential services", Definition = "Services to users of a system that must be provi ded even in the presence of intrusion, failure, or accident ", Source = "http://www.sei.cmu.edu/publications/documents/97.reports/97tr013/97tr013abstract.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "exposure", Definition = "The dissemination of information to anyone who is not authorized to access that in- formation ", Source = "http://www.cert.org/archive/pdf/OCTAVEthreatProfiles.pdf", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "fabrication", Definition = "Aims to fool other machines on the network into accepting the imposter as an origi- nal, either to lure the other machines into sending it data or to allow it to alter data ", Source = "http://www.cert.org/research/taxonomy_988667.pdf", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "fault line attacks", Definition = "Fault line attacks use weaknesses between interfaces of systems to exploit gaps in coverage ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "fault tolerance", Definition = "Describes a computer system or component designed so that, in the event that a com- ponent fails, a backup component or procedure can immediately take its place with no loss of service. Fault tolerance can be provided with software, or embedded in hardware, or provided by some combination ", Source = "http://watis.techtarget.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "firewall", Definition = "A system designed to prevent unauthorized access to or from a private network. Fire- walls can be implemented in both hardware and software, or a combination of both ", Source = "http://www.webopedia.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "hacker", Definition = "An individual who breaks into computers primarily for the challenge and status of obtaining access ", Source = "http://www.cert.org/research/JHThesis/Start.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "honey pot", Definition = "Programs that simulate one or more network services designated on a computer’s ports. An attacker assumes that vulnerable services that can be used to break into the machine are being run. A honey pot can be used to log access attempts to those ports, including the attacker’s keystrokes. This can provide advanced warning of a more concerted attack ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "HTTP header manipulation", Definition = "HTTP requests and responses send information in the HTTP headers. HTTP headers are a series of lines containing a name/value pair used to pass information such as the host, referrer, user agent, etc. HTTP headers can be manipulated to cause SQL injec- tion or cross-site scripting errors.", Source = "ASI", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "impact", Definition = "The negative effect of an attack on a victim system by an attacker ", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "incident", Definition = "An adverse network event in an information system or network or the threat of the occurrence of such an event ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "incident handling", Definition = "An action plan for dealing with intrusions, cyber theft, denial of service, fire, floods, and other security-related events ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "insider threat", Definition = "The threat that authorized personnel of an organization will act counter to the organi- zation’s security and interest, especially for the purposes of sabotage and espionage ", Source = "http://www.hpcc-usa.org/pics/02-pres/wright.ppt", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "integrity", Definition = "For systems, the quality that a system has when it can perform its intended function in a unimpaired manner, free from deliberate or inadvertent unauthorized manipula- tion. For data, the property that data has not been changed, destroyed, or lost in an unauthorized or accidental manner ", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "interception", Definition = "Access to an asset gained by an unauthorized party ", Source = "Security in Computing", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "intrusion", Definition = "An attack on a network for the purpose of gaining access to or destroying privileged information or disrupting services to legitimate users ", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "intrusion detection system", Definition = "A combination of hardware and software that monitors and collects system and net- work information and analyzes it to determine if an attack or an intrusion has oc- curred. Some ID systems can automatically respond to an intrusion ", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "intrusion prevention system", Definition = "A system used to actively drop packets of data or disconnect connections that contain unauthorized data. Intrusion prevention technology is also commonly an extension of intrusion detection technology ", Source = "www.wikipedia.org", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "liability", Definition = "The responsibility of someone for damage or loss ", Source = "http://www.sei.cmu.edu/publications/documents/03.reports/03hb002.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "luring attack", Definition = "A type of elevation of privilege attack where the attacker “lures” a more highly privi- leged component to do something on his or her behalf. The most straightforward technique is to convince the target to run the attacker’s code in a more privileged security context ", Source = "“Item 7: What is a Luring Attack?” The .NET Developer’s Guide to Windows Security", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "malware", Definition = "Programming or files that are developed for the purpose of doing harm. Thus, mal- ware includes computer viruses, worms, and Trojan horses ", Source = "http://www.webopedia.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "man-in-the-middle attack", Definition = "An attack in which the attacker is able to read, and possibly modify at will, messages between two parties without letting either party know that they have been attacked. The attacker must be able to observe and intercept messages going between the two victims ", Source = "http://encyclopedia.thefreedictionary.com/man%20in%20the%20middle%20attack", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "masquerade", Definition = "Aims to fool other machines on the network into accepting the imposter as an origi- nal, either to lure the other machines into sending it data or to allow it to alter data ", Source = "http://www.cert.org/research/taxonomy_988667.pdf", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "modification", Definition = "Situation in which an unauthorized party not only gains access to, but tampers with an asset ", Source = "http://www.cert.org/research/JHThesis/Start.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "non-essential services", Definition = "Services to users of a system that can be temporarily suspended to permit delivery of essential services while the system is dealing with intrusions and compromises ", Source = "http://www.sei.cmu.edu/publications/documents/97.reports/97tr013/97tr013abstract.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "non-repudiation", Definition = "The goal of non-repudiation is to prove that a message has been sent and received ", Source = "http://www.ssimail.com/Glossary.htm#N", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "patch", Definition = "A small update released by a software manufacturer to fix bugs in an existing pro- gram ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "patching", Definition = "The process of updating software to a new version that fixes bugs in a previous ver- sion ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "penetration", Definition = "Intrusion, trespassing, or unauthorized entry into a system ", Source = "http://www.yourwindow.to/information-security", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "penetration testing", Definition = "The execution of a testing plan, the sole purpose of which is to attempt to hack into a system using known tools and techniques ", Source = "http://www.yourwindow.to/information-security", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "physical security", Definition = "Security measures taken to protect systems, buildings, and related supporting infra- structure against threats associated with their physical environment ", Source = "http://encyclopedia.thefreedictionary.com/man%20in%20the%20middle%20attack", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "port scanning", Definition = "The act of systematically scanning a computer’s ports ", Source = "http://www.webopedia.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "privacy", Definition = "The quality or condition of being secluded from the presence or view of others ", Source = "http://dictionary.reference.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "procedure", Definition = "The implementation of a policy in the forms of workflows, orders, or mechanisms ", Source = "http://www.sei.cmu.edu/publications/documents/03.reports/03hb002.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "recognition", Definition = "The capability of a system to recognize attacks or the probing that precedes attacks ", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "recovery", Definition = "A system’s ability to restore services after an intrusion has occurred. Recovery also contributes to a system’s ability to maintain essential services during intrusion ", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "replay attack", Definition = "The interception of communications, such as an authentication communication, and subsequent impersonation of the sender by retransmitting the intercepted communi- cation ", Source = "http://www.ffiec.gov/ffiecinfobase/booklets/information_security/08_glossary.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "resilience", Definition = "The ability of a computer or system to both withstand a range of load fluctuations and also remain stable under continuous and/or adverse conditions ", Source = "http://www.yourwindow.to/information-security", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "resistance", Definition = "Capability of a system to resist attacks ", Source = "A Trustworthy Refinement Through Intrusion-Aware Design", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "risk", Definition = "The product of the level of threat with the level of vulnerability. It establishes the likelihood of a successful attack ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "risk assessment", Definition = "The process by which risks are identified and the impact of those risks determined ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "security policy", Definition = "A policy that addresses security issues ", Source = "http://www.sei.cmu.edu/publications/documents/03.reports/03hb002.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "script kiddies", Definition = "The more immature but unfortunately often just as dangerous exploiter of security lapses on the Internet. The typical script kiddy uses existing and frequently well known and easy-to-find techniques and programs or scripts to search for and exploit weaknesses in other computers on the Internet—often randomly and with little regard or perhaps even understanding of the potentially harmful consequences ", Source = "http://watis.techtarget.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "spoof", Definition = "The term is used to describe a variety of ways in which hardware and software can be fooled. IP spoofing, for example, involves trickery that makes a message appear as if it came from an authorized IP address ", Source = "http://www.webopedia.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "SQL injection", Definition = "A type of input validation attack specific to database-driven applications where SQL code is inserted into application queries to manipulate the database ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "stakeholder", Definition = "Anyone who is a direct user, indirect user, manager of users, senior manager, opera- tions staff member, support (help desk) staff member, developer working on other systems that integrate or interact with the one under development, or maintenance professionals potentially affected by the development and/or deployment of a soft- ware project ", Source = "http://www.agilemodeling.com/essays/activeStakeholderParticipation.htm", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "stealthing", Definition = "A term that refers to approaches used by malicious code to conceal its presence on an infected system ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "survivability", Definition = "The capability of a system to complete its mission in a timely manner, even if sig- nificant portions are compromised by attack or accident. The system should provide essential services in the presence of successful intrusion and recover compromised services in a timely manner after intrusion occurs ", Source = "http://www.sei.cmu.edu/publications/documents/03.reports/03tn013.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "target", Definition = "The object of an attack, especially host, computer, network, system, site, person, organization, nation, company, government, or other group ", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "threat", Definition = "A potential for violation of security, which exists when there is a circumstance, capa- bility, action, or event that could breach security and cause harm ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "threat assessment", Definition = "The identification of the types of threats that an organization might be exposed to ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "threat model", Definition = "Used to describe a given threat and the harm it could to do a system if it has a vulner- ability ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "toolkits", Definition = "A collection of tools with related purposes or functions, e.g., antivirus toolkit, disk toolkit ", Source = "http://www.yourwindow.to/information-security", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "Trojan", Definition = "A program in which malicious or harmful code is contained inside apparently harm- less programming or data in such a way that it can get control and do its chosen form of damage, such as ruining the file allocation table on a hard disk ", Source = "http://watis.techtarget.com", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "trust", Definition = "Determines which permissions other systems or users have and what actions they can perform on remote machines ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "uptime", Definition = "The property of a system or a system resource being accessible and usable upon de- mand by an authorized system entity, according to performance specifications for the system; i.e., a system is available if it provides services according to the system de- sign whenever users request them ", Source = "http://www.sei.cmu.edu/publications/documents/99.reports/99tr028/99tr028abstract.html", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "victim", Definition = "That which is the target of an attack. An entity may be a victim of either a successful or unsuccessful attack ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "virus", Definition = "A hidden, self-replicating section of computer software, usually malicious logic, that propagates by infecting—i.e., inserting a copy of itself into and becoming part of— another program. A virus cannot run by itself; it requires that its host program be run to make it active ", Source = "http://www.sans.org/resources/glossary.php", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "vulnerability", Definition = "A condition or weakness in (or absence of) security procedures, technical controls, physical controls, or other controls that could be exploited by a threat ", Source = "http://csrc.nist.gov/publications/nistpubs/800-12/handbook.pdf", SquareType = security });
            project.ProjectTerms.Add(new ProjectTerm() { Project = project, Term = "worm", Definition = "A self-replicating virus that does not alter files but resides in active memory and duplicates itself. Worms use parts of an operating system that are automatic and usu- ally invisible to the user. It is common for worms to be noticed only when their un- controlled replication consumes system resources, slowing or halting other tasks ", Source = "http://watis.techtarget.com", SquareType = security });
            #endregion

            // create the business/security goals
            var business = context.GoalTypes.Where(a => a.Id == GoalTypes.Business).Single();
            project.Goals.Add(new Goal(){Project = project, Description = "This tool is not intended to provide canned responses to every possible scenario, but instead provides the means to make informative decisions based on available sources.", Name="Business goal", GoalType = business, SquareType = security});

            var sgoals = context.GoalTypes.Where(a => a.Id == GoalTypes.Security).Single();
            project.Goals.Add(new Goal() { Project = project, Description = "Insure that information and resources are accessed only by those who are authorized to use them", Name = "Confidentiality", GoalType = sgoals, SquareType = security });
            project.Goals.Add(new Goal() { Project = project, Description = "Ensure that the Asset Management System is functional and available at all times. This includes the core facility management services, Sybase databases, etc.", Name = "Availability", GoalType = sgoals, SquareType = security });
            project.Goals.Add(new Goal() { Project = project, Description = "Integrity of data is absolutely critical in the Asset Management System package.  Data backups and checksum integrity verification are of the utmost importance", Name = "Data Integrity", GoalType = sgoals, SquareType = security });
            project.Goals.Add(new Goal() { Project = project, Description = "Preserve or enhance the ability to accurately record the activities that take place.", Name = "Monitoring", GoalType = sgoals, SquareType = security }); 
            project.Goals.Add(new Goal() { Project = project, Description = "Only authorized users of the Asset Management System have access to their specified and permissible resources", Name = "Access Control", GoalType = sgoals, SquareType = security });
            project.Goals.Add(new Goal() { Project = project, Description = "The most important goal is the ability to deliver essential services in the face of attack, failure, or accident.", Name = "Maintaining Mission Critical Services", GoalType = sgoals, SquareType = security });
            project.Goals.Add(new Goal() { Project = project, Description = "Have a current disaster recovery plan in the event of an emergency or service disruption.", Name = "Disaster Recovery", GoalType = sgoals, SquareType = security });
            project.Goals.Add(new Goal() { Project = project, Description = "Periodic code review should be performed to ensure script and code confidentiality and integrity", Name = "Code Review", GoalType = sgoals, SquareType = security });

            // select the elicitation and risk assessment types
            var elicitation = context.ElicitationTypes.Where(a => a.Name == "Not Listed" && a.SquareType.Id == security.Id).Single();
            project.SecurityElicitationType = elicitation;
            project.SecurityElicitationRationale = "This was the selected method used by the project team.";

            // insert the categories
            project.Categories.Add(new Category() { Name = "Disaster Control", Project = project, SquareType = security });
            project.Categories.Add(new Category() { Name = "Authentication", Project = project, SquareType = security });
            project.Categories.Add(new Category() { Name = "Unauthorized Attacks", Project = project, SquareType = security });
            project.Categories.Add(new Category() { Name = "Disaster Recovery", Project = project, SquareType = security });
            project.Categories.Add(new Category() { Name = "Encryption", Project = project, SquareType = security });
            project.Categories.Add(new Category() { Name = "Auditing", Project = project, SquareType = security });
            project.Categories.Add(new Category() { Name = "Privacy", Project = project, SquareType = security });
            project.Categories.Add(new Category() { Name = "Survivability", Project = project, SquareType = security });
            project.Categories.Add(new Category() { Name = "Access Control", Project = project, SquareType = security });

            // insert the requirements
            project.Requirements.Add(new Requirement() { RequirementId = "SU-3", RequirementText = "Only System Administrators are permitted to install any software and/or hardware", Name = "Administrators Install Software", Source = "PR-01, PR-12, MC 13, MC-15", Priority = 2, Order = 2, Project = project, Category = project.Categories.Where(a => a.Name == "Survivability").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "SU-4", RequirementText = "The operating system, applications, firewalls, and IDS must be patched routinely.", Name = "Patching", Source = "PR-02, PR-08, MC-01, MC-03, MC-13, MC-15, MC-16, MC-17, MC-18, MC-19, MC-20 MC- 21, MC-22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Survivability").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-9", RequirementText = "Follow the principle of lest privilege and use least privileged service account to run processes and access resources.", Name = "Prevent Input Validation Attack", Source = "PR-09, MC-08, MC-09, MC-11, MC-17, MC-20, MC-22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AU-5", RequirementText = "User activities must be periodically reviewed.", Name = "Activity Review", Source = "PR-04, PR-14, PR-21, MC-01, MC02, MC03, MC-04, MC-05, MC-06, MC-07, MC-08, MC-09, MC-10, MC-14, MC-15, MC-16, MC-17, MC-18, MC-20, MC-22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Auditing").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AU-1", RequirementText = "The system shall audit systems on network and user logging information.  This shall be put into practice monthly.", Name = "Audit User Information", Source = "AR-03, MC-01, MC-02, MC-03, MC-04, MC-05, MC-06, MC-07", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Auditing").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "PV-6", RequirementText = "Require users to change their passwords periodically.", Name = "Change Passwords", Source = "PR-16, PR-24, MC-01, MC-03, MC-10, MC-20", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Privacy").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-6", RequirementText = "The system shall enforce the use of firewall technology.", Name = "Firewall", Source = "AR-26, MC-17, MC-18", Priority = 2, Order = 2, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AC-5", RequirementText = "Users should not have rights or access levels beyond those which are prescribed by their job responsibilities.", Name = "Rights by Job", Source = "PR-23, MC-01, MC-02, MC03, MC-06, MC-08, MC-09, MC-10", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Access Control").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AC-2", RequirementText = "The system shall not grant any client application access to one or more system", Name = "Client Application Access", Source = "AR-08, MC-08", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Access Control").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AN-2", RequirementText = "Authentication control mechanism shall be enforced in production environment. Authentication control will be done on the network level.", Name = "Network Authentication", Source = "AR-04, AR-09, MC-17, MC-19", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Authentication").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "EN-3", RequirementText = "A secure communication channel shall be used to secure Web data transfer.", Name = "Secure Data Transfer.", Source = "AR-32, MC-12, MC-13,MC-19", Priority = 2, Order = 2, Project = project, Category = project.Categories.Where(a => a.Name == "Encryption").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AU-7", RequirementText = "A DBA and/or manager performs information integrity checks on a routine basis.", Name = "Integrity Checks", Source = "PR-04, PR-14, PR-21, MC-01, MC02, MC03, MC-04, MC-05, MC-06, MC-07, MC-08, MC-09, MC-10, MC-14, MC-15, MC-16, MC-17, MC-18, MC-20, MC-22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Auditing").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "PV-1", RequirementText = "The system shall not permit any user login data to be retrieved by an attacker.", Name = "Protect login data", Source = "AR-10, AR-17, MC-20", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Privacy").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "SU-6", RequirementText = "Do not set up shared files/folders/drives on the network.", Name = "No shared files/folders/drives", Source = "PR-06, MC-08, MC-10, MC-11, MC-14", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Survivability").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "SU-5", RequirementText = "New systems on the network should be evaluated prior to deployment.", Name = "New System Evaluation", Source = "PR-11, MC-17", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Survivability").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AC-1", RequirementText = "The system shall not grant any user access to one or more system services and data during a secure session without first identifying the user.", Name = "User Access", Source = "AR-08, MC-08", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Access Control").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AN-1", RequirementText = "Authentication control mechanism shall be enforced in production environment. Authentication control will be done on user name and password or  other user credentials.", Name = "Authentication Controls", Source = "AR-01, MC-01", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Authentication").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "PV-3", RequirementText = "Password-protect any necessary shared documentation", Name = "Password Shared Documentation", Source = "PR-07, PR-13, PR-22, MC-01, MC-03, MC-06, MC-08, MC-09, MC-10, MC-11, MC-13, MC-14, MC- 20", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Privacy").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "SU-2", RequirementText = "All installation must be approved and reviewed by managers.    ", Name = "Managers Review Installation", Source = "PR-01, PR-12, MC-13, MC-15", Priority = 2, Order = 2, Project = project, Category = project.Categories.Where(a => a.Name == "Survivability").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "PV-5", RequirementText = "Users should not reveal their account names and passwords in any situation", Name = "Credentials Protection", Source = "PR-16, PR-24, MC-01, MC-03, MC-10, MC-20", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Privacy").Single(), SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AC-4", RequirementText = "Set clear and defined user access control for all users (Low, Medium, High, System Admin).", Name = "User access levels", Source = "PR-19, MC,01, MC-08, MC-10, MC-11, MC-20", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Access Control").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "EN-2", RequirementText = "Secure technologies shall be used to provide secure communications channels.", Name = "Secure Communication Channels", Source = "AR-25, MC-13, MC-19", Priority = 2, Order = 2, Project = project, Category = project.Categories.Where(a => a.Name == "Encryption").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-10", RequirementText = "Perform routine code review", Name = "Code Reviews", Source = "PR-15, MC-16", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "SU-1", RequirementText = "The system shall continue to fulfill its mission in the presence of an attack (possibly in minim and safe mode).", Name = "Survive Attack", Source = "AR-21, AR-22, MC-21", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Survivability").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-2", RequirementText = "The system shall be able to determine when a buffer overflow attack has  occurred. If attack takes place, system should shut down and system administrator  should be notified within a reasonable time.", Name = "Buffer Overflow", Source = "AR-05, MC-16", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AU-6", RequirementText = "Configuration changes are stored and cross-reviewed.", Name = "Configuration Review", Source = "PR-04, PR-14, PR-21, MC-01, MC02, MC03, MC-04, MC-05, MC-06, MC-07, MC-08, MC-09, MC-10, MC-14, MC-15, MC-16, MC-17, MC-18, MC-20, MC-22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Auditing").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-1", RequirementText = "The system shall protect itself from viruses by using virus detection  software with updated signatures. The virus detection software should also be run  weekly.", Name = "Virus Protection", Source = "AR-02, MC-17", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-5", RequirementText = "The system shall protect itself from input validation attack.", Name = "Input Validation", Source = "AR-16, MC-16", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-7", RequirementText = "A form of intrusion detection shall be implemented in the system. If the system detects any corruption of data or messages, then the system shall record the security event; notify the system administrator in a timely manner.", Name = "Intrusion Detection", Source = "AR-27, MC-21, MC-18", Priority = 2, Order = 2, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AU-3", RequirementText = "Log all incoming and outgoing traffic.", Name = "Log Traffic", Source = "PR-03, PR-10, MC-01, MC-02, MC-03, MC-04, MC-05, MC-06, MC-07, MC-08, MC-09, MC- 10, MC-11, MC-12, MC-13, MC-16, MC-17, MC-18, MC-19, MC-22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Auditing").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-4", RequirementText = "The system shall protect itself from attacks due to weak server configuration.", Name = "Server Configuration", Source = "AR-15, MC-17", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "DR-1", RequirementText = "Develop disaster recovery and contingency plan.    DR-2) Perform routine system and data backup.", Name = "Recovery", Source = "PR-05, PR-20, MC-01, MC-02, MC-03, MC-04, MC-05, MC-06, MC-07, MC-08, MC-09, MC- 10, MC-14, MC-15, MC-16, MC-17, MC-18, MC 21, MC-22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Disaster Recovery").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AU-4", RequirementText = "Separate personnel review system administrator activities", Name = "Admin Review", Source = "PR-18, MC-02, MC-04, MC-05", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Auditing").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-3", RequirementText = "The system shall protect itself from malicious encoding mechanisms.", Name = "Protect Malicious Encoding", Source = "AR-13, AR-14, AR-23, AR-24, AR-30, AR-31, MC22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AC-3", RequirementText = "If no identification is provided, the system shall record the security event and notify the operator within a reasonable time", Name = "No Identification", Source = "AR-08, MC-08", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Access Control").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "EN-1", RequirementText = "The system’s data and communication shall be encrypted.", Name = "Encrypt Communications", Source = "AR-12, MC-10", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Encryption").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "PV-4", RequirementText = "Users should log out of AMS system or close browser as soon as their activities are done", Name = "Logout Rule", Source = "PR-07, PR-13, PR-22, MC-01, MC-03, MC-06, MC-08, MC-09, MC-10, MC-11, MC-13, MC-14, MC- 20", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Privacy").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "AU-2", RequirementText = "Audit information must be reviewed routinely.    ", Name = "Audit Logging", Source = "PR-03, PR-10, MC-01, MC-02, MC-03, MC-04, MC-05, MC-06, MC-07, MC-08, MC-09, MC- 10, MC-11, MC-12, MC-13, MC-16, MC-17, MC-18, MC-19, MC-22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Auditing").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "PV-2", RequirementText = "Enforce strong password policies.    ", Name = "Password Policies", Source = "PR-07, PR-13, PR-22, MC-01, MC-03, MC-06, MC-08, MC-09, MC-10, MC-11, MC-13, MC-14, MC- 20", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Privacy").Single() , SquareType = security });
            project.Requirements.Add(new Requirement() { RequirementId = "UA-8", RequirementText = "The system shall protect itself from input validation attack by assuring file names are well formed.", Name = "File name format", Source = "AR-35, AR-36, MC-16, MC-22", Priority = 3, Order = 1, Project = project, Category = project.Categories.Where(a => a.Name == "Unauthorized Attacks").Single() , SquareType = security });

            // save

            // save the project
            context.Projects.Add(project);

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw;
            }

        }
    }
}