# Blue Prism Search Shortest Path

Project for blue prism for avaliation purpose. The goal of this project is to receive a list of words from a file, read the list, and write to a file the shortest path between those words.


## Usage

on the root you can open BluePrism.TechTest\BluePrism.TechTest.sln
or run
dotnet run {arg0} {arg1} {arg2} {arg3} --project BluePrism.TechTest\BluePrism.TechTest\BluePrism.TechTest.csproj

## Arguments
- arg0 = start word
- arg1 = end word
- arg3 = file location of list of words
- arg4 = output file to save shortest path

## How did approach the problem
- First load the words from a file using FileRepository of words
- Then Build a tree node using "Breadth First Search" approach instead of "Depth First Search", which allow me to remove imediatly the words from a remaining closest possible words(Word class and not string).
- Then use a implementation of "Search Shortest Path" using "Depth First Search" and every time that I found the "end" word, I simple had the node the possible path with number of words that I had to throw until there. Note: To found the closest words I use a Comparer implementation, so if we want to change that logic on "how we find shortest", we can change it easily.  
- After getting the "end" nodes, I was possible to write the shortest path using LIFO(Last in First Out) approach by navigating until the "start" node using the "Parent" property.
- To write the file just use a specific implementation for it, which allow to write to a file base on the settings

## Patterns
- DDD (Domain Driven Design)
- Repository Pattern
- Mediator
- CQRS (Command Query Single Responsibility)

## Plugins
- Fluent Validations 

## Extra notes
Behaviors Pipelines to improve and simplify command logs and validations
I could also add "AllowLengthWords" as a setting but instead I decide to use constants.