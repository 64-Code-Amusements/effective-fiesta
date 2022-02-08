---
layout: post
title: "Closures in C#"
categories: 
  - dotnet
  - functional
author: "walt"
date: 2022-02-05
comments: true
---

While C# is not per say a functional language there are tools at our disposal that can make it seem more like one.  _Closures_ are an oft used _functional programming_ technique that we can use quite easily in _C#_.

What are _closures_?  The [Wikipedia](https://en.wikipedia.org/wiki/Closure_(computer_programming)) definition reads as: "... a closure ... is a technique for implementing lexically scoped name binding ...".  This seems a little heavy.  In simpler terms, a _closure_ is a function that seals over a variable that was available when it was created but not when it is executed.  Sounds a little like a class with a static factory method to create instances.

### tl;dr

This is the closure:

```csharp
Func<int, int> GetIncrementor(int step) => x => x + step;
```

And this is how we use it:

```csharp
var inc = GetIncrementor(1);
var dec = GetIncrementor(-1);
// ...
inc(10).Should().Be(11);
dec(10).Should().Be(9);
```

The one line closure can be a local function or a class level method.
  
### Why Closures?

What we end up with is a succinct and expressive way to indicate intent.  Carrying on from the example code - have a look at the [GitHub repo](https://github.com/floatingman-ltd/effective-fiesta) - we can create an incrementor for any value.

```csharp
var incBy5 = GetIncrementor(5);

incBy5(5).Should().Be(10);
```

This means with two lines of code we clearly show intent.  Okay, three lines if we want to include the delegate declaration.  

But where's the reusability?  This code is all in one class, won't I end up copy pasting this all over the place?  Well, yes and no, you could copy the specific use around, it's only a line of code or you could leverage the `static` keyword for usings.  Put all of your _closures_ in on static class and then import that class as a static.

The closures class:

```csharp
namespace Floatingman.Common {
  public static class Closures{
    public static Func<int, int> GetIncrementor(int step) => x => x + step;
  }
}
```

Usage in code:

```csharp
using static Floatingman.Common.Closures;

//  later on ...

var incBy5 = GetIncrementor(5);

incBy5(5).Should().Be(10);
```

Yeah, this is starting to move away from succinct as add move boiler plate around the implementation.  My bad, the incrementor is an over simplistic closure with a wider usage.  If this were instead in the form of a local function, performing a task specific to the containing function and no where else the value shifts again.  

Further application is in the fact that this is a first class function and can be passed around as a value and executed latter.  It is (in this case) a `Func<int, int>` and can be passed as an argument when that signature is required.  

#### Static Factory Method

For clarity I'll cover off the earlier statement about the static factory method.  It would look like this:

```csharp
public class Incrementor{
  private int _step;

  public static Incrementor GetIncrementor(int step)
    => new Incrementor(step);

  private Incrementor(int step) {
    _step = step;
  }

  public Step(int value)
    => value + _step;
}
```

And in usage:

```csharp
var incBy3 = Incrementor.GetIncrementor(3);
var decBy3 = Incrementor.GetIncrementor(-3);

incBy3.Step(7).Should().Be(10);
decBy3.Step(13).Should().Be(10);
```

This is the argument for **succinct**, seven lines of code to do the same work as the one line function.  Even the **heavy** solution I suggested that utilizes a static class for the closures is only one line of code.

Suppose suddenly you have a need to increment doubles, this becomes problematic with trying to use generics, so we end up re-implementing the static factory class again for doubles.  Or again a single line of code for the closure, which because of overloading can happy live right beside the `int` version.  And, the calling code doesn't even need to know whether it needs the `int` or the `double` version.

```csharp
Func<double,double> Incrementor(double step) => x => x + step;
Func<int, int> GetIncrementor(int step) => x => x + step;
```

And this is how we use it:

```csharp
var inc = GetIncrementor(1);
var dec = GetIncrementor(-1.21);
// ...
inc(10).Should().Be(11);
dec(10).Should().Be(8.79);
```

### How Closures?

Alright, how did we end up with our one line of code?  

#### Long Form Factory Class

Let's start with the long form static factory class.  I call this the long form because I have removed as much of the syntactic sugar as possible, this class should be readable and understandable by anyone that can read C#.

```csharp
namespace Floatingman.Common{
  public static class Incrementor_LongForm{
    private int _step;

    private Incrementor(int step){
      _step = step;
    }
    
    public static Incrementor GetIncrementor(int step){
      return new Incrementor(step);
    }
    
    public int Increment(int value){
      return _step + value;
    }
  }
}
```

The tests for this show that behaves as expected:

```csharp
var incBy3 = Incrementor.GetIncrementor(3);
var decBy3 = Incrementor.GetIncrementor(-3);

incBy3.Step(7).Should().Be(10);
decBy3.Step(13).Should().Be(10);
```

#### Short Form Factory Class

There is no functional difference between these two blocks of code as the tests shows, the final usage is the same.  What we've done is use the lambda operator, in effect this a step towards functional.  The methods would read as 'goes to', so, "step value goes to ...".  

```csharp
public class Incrementor{
  private int _step;

  public static Incrementor GetIncrementor(int step)
    => new Incrementor(step);

  private Incrementor(int step) {
    _step = step;
  }

  public Step(int value)
    => value + _step;
}
```

And in usage:

```csharp
var incBy3 = Incrementor.GetIncrementor(3);
var decBy3 = Incrementor.GetIncrementor(-3);

incBy3.Step(7).Should().Be(10);
decBy3.Step(13).Should().Be(10);
```

#### Named Delegates

A delegate is a place holder for a function, `delegate` has been a keyword since the beginning and was early on often used in event driven programming.  We can declare a top level type as a delegate in the either of the above classes.  In the following code, the `delegate int ...` is the declaration of a type of function that accepts a single parameter.

```csharp
namespace Floatingman.Common{

  delegate int Transformer(int x);
  
  public class Incrementor{
    // Rest of class omitted
    // ...

    Transformer GetTransformer Step;
  }
}
```

Our tests are a little different here, we know the incrementor works, this is about the delegate.  The Need to call `Step` has been removed because we are returning an actual callable delegate.

```csharp
var inc= Incrementor.GetIncrementor(1);
var callInc = inc.GetTransformer;

callInc(10).Should().Be(11);
```

What's important at this step is the fact that we have a function `callInc` that we can call or pass around.  It's important to realize that the delegate has not be called or executed until it's used, so `callInc` is just an instance of a `delegate` type.  Also, it exists out side the context of the class.  So if we contain the class within a scope we still have access to the delegate even though the instance is out od scope.

```csharp
Transformer callInc;
{
  var inc = Incrementor.GetIncrementor(2);
  callInc = inc.GetTransformer();
}
callInc(10).Should().Be(12);
```

#### Anonymous Delegates

On more step on our journey to the ~~dark~~ functional side.  The introduction of anonymous functions (and methods) means we can dispense with the `delegate` type declaration, we can even pass that down to the point of use.

```csharp
Func<int,int> callInc;
{
  var inc = Incrementor.GetIncrementor(2);
  callInc = inc.GetTransformer();
}
callInc(10).Should().Be(12);
```

Finally, we can combine the anonymous function syntax with the lambda syntax.  The transformation of the `GetTransformer` method evolves as follows:

```csharp
// current
Func<int, int> GetIncrementor(int step){
  var _step = step;
  Func<int, int> lambdaFunc = x => x + _step;
  return lambdaFunc;
}

// 1st reduction
Func<int, int> GetIncrementor(int step){
  return x => x + step;
}

// final state
Func<int, int> GetIncrementor(int step)
  => x => x + step;
```

If you're not familiar with the `Func<~>` (or the `Action<~>`) keywords in C#, they are delegates.  The [serious documentation](https://docs.microsoft.com/en-us/dotnet/api/system.func-2?view=net-6.0) can be a little over whelming.  So let's back this up all the way to the beginning of _delegates_.

### That's All

Since we've reduced the external class to one line it seems a little heavy to need to even keep that much extra.  I'll leave that discussion for a future post.  

Poke around in the repo, I've left a couple of edge cases in there.  Let me know what you think in the comments below.

---

\* code shapes - as far as I know this is a new usage of the word, what I'm implying with the term is a micro-pattern.  A way to write some code to perform some very specific task.  A step beyond stylistic preferences and before algorithms and patterns.
