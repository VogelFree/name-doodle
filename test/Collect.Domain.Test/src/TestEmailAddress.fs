namespace NameDoodle.Domain.Collect.Test

open NameDoodle.Domain.Collect.Common
open NUnit.Framework

[<TestFixture>]
type TestEmailAddress () =

    let validEmailString = "Hans.Peter@web.de"
    let noAtEmailString = "Hans.Peter.web.de"

    let assertEqual (expectedValue : string) =
        fun (value : string) -> Assert.That(value, Is.EqualTo(expectedValue))

    [<Test>]
    member this.TestCreate () =
        validEmailString
        |> EmailAddress.create
        |> fun emailResult -> 
            match emailResult with
                | Ok email -> 
                    email 
                        |> EmailAddress.value 
                        |> assertEqual validEmailString
                | _ -> Assert.Fail()

    [<Test>]
    member this.TestCreateNoAt () =
        noAtEmailString
        |> EmailAddress.create
        |> fun emailResult ->
            match emailResult with
                | Error emailError -> 
                    Assert.That(emailError, Is.TypeOf<InvalidEmailAddressError>())
                | _ -> Assert.Fail()