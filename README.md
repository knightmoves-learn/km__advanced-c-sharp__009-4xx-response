# 009 4xx Response

## Lecture

[![# 009 4xx Response](https://img.youtube.com/vi/1a5s6qqXh7Y/0.jpg)](https://www.youtube.com/watch?v=1a5s6qqXh7Y)

## Instructions

In this assignment you will implement returning a `404: Not Found` result in HomeEnergyApi.

In `HomeEnergyApi/Controllers/HomesController.cs`...

- Update the `FindById()` method.
  - Should respond with `404: Not Found` when the no home in `homesList` has the `Id` being passed.
- Update the `UpdateHome()` method.
  - Should respond with `404: Not Found` when the no home in `homesList` has the `Id` being passed.
- Update the `DeleteHome()` method.
  - Should respond with `404: Not Found` when the no home in `homesList` has the `Id` being passed.

Additional Information:

- You should ONLY make code changes in `HomeEnergyApi/Controllers/HomesController.cs` to complete this assignment.

## Building toward CSTA Standards:

- Explain how abstractions hide the underlying implementation details of computing systems embedded in everyday objects (3A-CS-01) https://www.csteachers.org/page/standards
- Demonstrate code reuse by creating programming solutions using libraries and APIs (3B-AP-16) https://www.csteachers.org/page/standards

## Resources

- https://en.wikipedia.org/wiki/List_of_HTTP_status_codes
- https://en.wikipedia.org/wiki/Request%E2%80%93response

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
