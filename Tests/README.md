# Tests — Repository<T> (Oppgave 6)

## Updated after feedback

I got feedback on the first version and had to fix two things:

1. **GetAll exposed the internal list.** It did `return items`, so code outside
   the class could change the repository through the list it got back.
   Now GetAll returns a copy, and I added a test
   (`GetAll_ModifyingReturnedList_DoesNotChangeRepository`) that changes the
   returned list and checks that the repository still has the original items.

2. **Not enough data-driven tests.** I changed `Repository_WorksWithStrings_NotJustSongs`
   and `Get_WithInvalidIndex_ThrowsException` to use `[Theory]` + `[InlineData]`,
   so the same test runs with different values instead of hardcoding them.

## How to run

From the `Oppgave6` folder:

```
dotnet test Tests
```

There are 8 tests in total (6 test methods, two of them run twice with `[InlineData]`).

## What is tested

**A. Basic functionality**
- `Add` increases `Count` and the item can be found with `Get`.
- `Remove` removes the right song (the Italian one) and `Count` goes down.
- `Clear` empties the repository.
- `GetAll` returns a copy, so the internal list can't be changed from outside.

**B. Generic**
- `Repository<T>` also works with `string`, not just `Song`.

**C. Edge cases**
- `Get` with an invalid index (-1 and 99) throws `ArgumentOutOfRangeException`.

## Reflection

**Why these tests?** I tried to test everything the class actually does: adding,
getting, removing, clearing, that it works with more than one type, and what
happens if you use a wrong index. The copy test makes sure nobody can mess
with the repository from outside.

**Did I cover all the basic functionality?** I think so. Every public method
(`Add`, `Get`, `GetAll`, `Count`, `Remove`, `Clear`) is tested at least once.

**Could another developer understand the class from the tests?** I think yes.
The test names say what should happen, and each test is a small example of
how to use the class.