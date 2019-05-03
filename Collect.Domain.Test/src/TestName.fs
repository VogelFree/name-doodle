namespace NameDoodle.Domain.Collect.Test

open NameDoodle.Domain.Collect.Common
open NUnit.Framework

[<TestFixture>]
type TestName () =

    [<TestCase("Hans")>]
    [<TestCase("Hans-Peter")>]
    member this.TestCreate (nameString) =
        nameString
        |> Name.create
        |> fun name ->
            match name with
            | Ok name -> Assert.That((Name.value name), Is.EqualTo(nameString))
            | _ -> Assert.Fail()

    [<TestCase("12345")>]
    [<TestCase("Hans1234")>]
    [<TestCase("/§$%")>]
    member this.TestCreateInvalid (nameString) =
        nameString
        |> Name.create
        |> fun name -> 
            match name with
            | Error error -> Assert.That(error, Is.TypeOf<InvalidNameFormatError>())
            | _ -> Assert.Fail()