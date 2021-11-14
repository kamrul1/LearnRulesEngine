# Rules Engine

Tutorial Link: https://app.pluralsight.com/library/courses/c-sharp-design-patterns-rules-pattern/table-of-contents
Souce Link: https://github.com/ardalis/DesignPatternsInCSharp/tree/master/DesignPatternsInCSharp/RulesEngine


This is a [behavioral design pattern](https://refactoring.guru/design-patterns/behavioral-patterns) and is 
used to model behaviour of a part of a system.

Rules engine is similar to the Specification Design Pattern, this only deals with individual rule rather then a collection as done by
Rules engine.



## Problem Rules Engines solve
  
Eliminate complex conditional logic into modular/maintainable code

## Apply the Rules Engine pattern

**Engine Part** - processes the rules and applies them to produce a result

**Rules Collection** - multiple parts that describe the condition, are usually grouped together

**System Input**  - information that needs to be processed e.g. customer/context discount


# Defining Rules

> Each rule should follow single responsiblity principle
> Rules are managed using an engine that chooses which rule(s) to apply
> Rules may be ordered, aggregated, or filtered as appropriate


A way to check if a method is go complex is to use the VS2022 menubar ```Analyze>Calculate Code Metrics>For Solution```

Any code with Cyclomatric Complexity>10 will likely violates Open/Closed Principles.  You should try to refactor.

## Working Rules 

- Keep individual rules simple
- Allow for complexity through combinations of simple rules
- Decide how rules will combine or be chosen
- Consider whether rule ordering will matter in evalution

## Steps to applying rules

- Have tests, so that when rules applied you will know if rules are broken
- Extract methods to have common signature e.g. evaluate interface
- Replace original method logic with call to Rules engine

See all iteration of the [Rule Engine development with githistory](https://github.githistory.xyz/kamrul1/LearnRulesEngine/blob/master/RulesEngine/DiscountCalculator.cs)

## Other notes
```
List method
AddRange, InsertRange. AddRange adds an entire collection of elements. 
It can replace tedious foreach-loops that repeatedly call Add on List.
```



