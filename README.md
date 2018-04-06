# Frends.Community.CombineXML
FRENDS task for combining 2 or more XMLs to 1

- [Installing](#installing)
- [Tasks](#tasks)
     - [CombineXML](#combinexml)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed
'Insert nuget feed here'

# Tasks

## CombineXML
Combines two or more xml strings or xml documents to one xml string

### Properties

#### Input

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| InputXmls | InputXml[] | Xml strings or xml documents that will be merged | n/a |
| XmlRootElementName| string | Root element of xml| 'Root' |

#### InputXml

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Xml| object | Xml input as string or xml document | '\<note>\<body>Hello\</body\</note>' |
| ChildElementName| string | Child element name where the xml document will be written in| 'ChildElement1' |


### Returns

Xml string of combined xml


# Building

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.CombineXML.git

Restore dependencies

`nuget restore FreFrends.Community.CombineXML`

Rebuild the project

Run Tests with nunit3. Tests can be found under

`Frends.Community.CombineXML\bin\Release\Frends.Community.CombineXML.Tests.dll`

Create a nuget package

`nuget pack Frends.Community.CombineXML.nuspec`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ----- | ----- |
| 1.0.0 | First version. 

