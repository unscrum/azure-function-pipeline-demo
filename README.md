# Strategy - Kickoff then Scale

### Problem Statement 
> A potential client, XYZ, is a traditional on prem infrastructure shop that is interested in moving to the cloud as well as modernizing their product suite and architecture. Developers have been complaining about kickstarting new environments, long lead times and cycles for development, and lack of consistency between their environments. Operations has also been complaining about downtime during deployments and code quality being released into production.

> Liatrio has been tasked with showing XYZ how to potentially do this. They want to migrate to the cloud and they’ve recently been interested in containerization.

---
# Executive Summary

XYZ has undertaken an initiative to modernize their product suite as well as move it Microsoft Azure.  XYZ has partnered with Liatrio to deliver a framework for delivering cloud based applications to Azure in alignment with the compliance requirements.  XYZ and Liatrio teams will focus on reducing development cycle times and bugs introduced to production by piloting a delivery pipeline that focuses on automating tests and deployments across environment.

## Strategy 
Following Liatrio's proven delivery acceleration model, Ignite, a Liatrio team will work along side a XYZ team with the goal of taking a single application, the _Lighthouse_, though a transformation to a cloud delivery model. 

## Discovery Phase
To begin the engagement, a two to three week discovery phase will help Liatrio assess the capabilities required to be built into a new CI/CD pipeline and the cloud capabilities required to securely host and operate the _Lighthouse_ application.  XYZ will come with 2-4 applications that are candidates to become the _Lighthouse_ application.  Some IaC and POC work will be started at this time.  

## Delivery Phase
Liatrio will deliver some _Infrastructure-as-Code_ and _Pipeline-as-Code_ artifacts in addition to training the XYZ team members on managing the IaC and PaC.  Dojos will take place during this time as well to help uplift XYZ team members skill-sets and generate momentum for the engagement and excitement throughout XYZ.

### Code Deliverables
- IaC required to support an initial Azure Landing Zone to host the _Lighthouse_ application.
- Pipeline to promote and the IaC to at least 2 environments with security controls
- Pipeline to Build, Test, Package and Promote the Application Code into the Azure Landing Zone to at least 2 environments with security controls and promotion approvals.

### Dojos
Company XYZ's initial _Lighthouse_ team will be taking though three Dojos as part of the transformation during the **Build** phase.

1. **TDD/BDD Dojo** - 2 to 3 working sessions designed to engage XYZ developer and QA team members to understand the concepts of Test Driven Development with an introduction into SpecFlow - A .NET BDD tool that supports the Gherkin/Cucumber language. 

2. **Infrastructure as Code  (IaC)/ Pipeline as Code (PaC) Dojo** - 1 to 2 presentations plus _Paired Programming_ throughout the engagement designed to help developer and operations team members to understand Infrastructure as Code and how Azure Pipelines can not only build and run unit tests, but enable security gates and promote tested code artifacts to be deployed across different environments, ultimately to production.

3. **Azure Dojo** - 3 to 5 training sessions on the different Azure resources required to securely deploy and operate the _Lighthouse_ application in Microsoft Azure. At least one of the training sessions will focus on FinOps and the others will be more technical in nature.

## Team Makeup
The initial _Lighthouse_ team will be made up a a combination of both Liatrio team members and XYZ team members.  This approach maximize opportunities for _Paired Programming_ and knowledge transfer.  

### Liatrio Team Members
- Technical Principal - Lead the engagement
- Sr. Consultant/Dojo Coach - .NET TDD/BDD with SpecFlow, Unit testing, integration testing  _(During Discovery and Dojo only)_
- Sr. Consultant/Dojo Coach - Azure IaC and Azure Pipeline expert _(During Discovery and Delivery)_
- 2 DevOps Consultants _(During Delivery)_

### XYZ Team Members
- 2-3 Developers with a desire to learn TDD. 
- 1-2 QA / Testers or Test Engineers with a desire to learn Gherkin/Cucumber syntax.
- 1-2 Operation team members with a desire to learn automation.
- 1 Architect - Ability to understand ans sign off on the new cloud application architecture.  _(Minimum 50% availability)_
- 1 Security team member - Ability to understand and sign off on security requirements. _(Minimum 50% availability)_  
- 1 DBA - if needed for the _Lighthouse_ application chosen _(Minimum 50% availability)_


## Milestones
1. Lighthouse application defined and Azure Architecture approved.
2. Initial pipeline that builds IaC and deploys Azure Landing Zone for the _Lighthouse Application_ promoted to at least 2 environments.
3. Initial pipeline that deploys the _Lighthouse Application_ into the Azure Landing Zone.
4. Completion of Dojos.
5. Sign-off with Security team member and architect on Azure controls and pipeline controls.

## Assumptions
- Liatrio team members will not write or refactor application code, but will coach on best practices for testing code.
- XYZ will make every effort to on-board Liatrio team members with access required, including access to Azure.
- XYZ team members will be available and will make decisions in a timely fashion.


## Expansion
During the engagement additional requirements and opportunities may be presented.  These may be addressed with a Change Control or optionally a new SOW.  Ultimately we know that there are 4 major products, and we are only moving a single _Lighthouse_ to a landing zone for this engagement.  Working off of the success & findings of this initial engagement, and having the opportunity to prove ourselves will hopefully win subsequent engagements for multiple _Lighthouse_ teams. This is how we help XYZ accelerate their cloud adoption.

___
## Appendix

### Chosen Option - Option 2 - Strategy to Kickoff then Scale
Build out the strategy to kick off an engagement and scale your solution across the enterprise.

- How many people would you need to deliver and what skill sets ?
- What milestones would you target for the initial offering ?
- How would you propose expanding the engagement?

Make sure to include team size, composition and the client team representatives. Ensure you
address Liatrio staffing personas, duration of engagement and anything else the client may find
important. Address the following concerns to start with:

- What is your approach for exciting the customer to embark on the journey?
- What do you need from the customer before kicking off the engagement ?
- What is needed during the execution?
- What is your scaling strategy ?
- How will you measure success 

### Requirements
- Commit a README file to a public git repository with an executive summary 
- A presentation (Miro, Mural, Slides, README, or medium of your choice) showing an engagement strategy from kickoff to scale and extension
- Some things to think about:
    - How would you share your strategy as an expert to our clients and get them excited?
    - How would you push/drive and scale your solution/approach across the enterprise?
    - How would you roll this delivery approach at a Fortune 100?

### Q & A
1. For company XYZ, What type of toolsets are employed today in their enterprise?  What is the dev stack?  What type of monitoring tools do they use?  What does their current SDLC look like? 
    > .NET, use app dynamics and want to move to Datadog with the modernization effort, they use the classic Azure DevOps pipelines for CI (clicky clicky UI not as code) and they manually deploy.

2. Developers are complaining about environments... Do we have any insight into how many static vs ephemeral environments they have now vs want to have?
    > Four static environments. A single dev environment that’s always changing and nobody knows what’s there day-day. It’s all manual and they have no way to create ephemeral environments or any environments via automation at all.

3. Ops is complaining about quality.  Do they have any metrics around code quality?  Is the  development team writing any unit/integration tests?  Is performance testing considered today?
    > No good metrics, we heard there were some tests, perf testing would be nice but they don’t have an app architecture that supports atomic tests.

4. What is operations comfort level with automation platforms for desired state configuration?
    > Currently no automation outside of some scripts - some ansible maybe but not more than a few examples.

5. I'm also curious about the number of applications and development teams that support them.  Is is a single app, or multiple? Is there single dev team or multiple?  This will help me to determine an approach for option 2
    > How about 4 major/primary products and 12 dev teams. There are probably 30-40 “apps” that make up the products as well as DBAs, networking, and security teams.

6. As Far as Roles/Titles at Liatrio, I understand there is a Principal, but what is a typical lighthouse team make up? I see open positions for Sr DevOps Engineer, DevOps Tech Lead & DevOps Engineer. Do our DevOps Engineers only do IaC and pipeline work, or do they have software development / testing skills as well?  Its hard to tell from the open position descriptions.  Is there another role for things like Software Developer Coach, or TDD Coaching, or is that rolled up under the DevOps Engineer?
    > We don’t write our customers’ software but several of our team have a development background and are capable. There are plenty of TDD and dev coach team members. Anyone on the team could play one of those coach roles, yes. We are would be player/coaches. We have a few folks that lean more one way than the other but the makeup of a team for a Dojo for example could have someone more coachy and someone more technically driven depending on the team and uplift required.

7. Is the make up like 1 tech-lead, 1-2 Sr DevOps Eng and 1-2 DevOps Eng or something similar?
    > Yeah for a normal lighthouse engagement we’d probably start with 1 Tech Principal (could be the tech lead as well) and 1-2 Sr. Eng and 1-2 Eng.  4-5 people. When we expand and have multiple teams going the teams may be smaller at 2-3 people on 3-5 teams +

8. Has the client picked a cloud, yet?  Azure seems like a good move given we established a .NET shop.
    > Sure they have some azure. Makes sense

