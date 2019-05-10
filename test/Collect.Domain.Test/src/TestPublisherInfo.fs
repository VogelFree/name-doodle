namespace NameDoodle.Domain.Collect.Test

open NameDoodle.Collect.Domain.PublisherInfo
open NameDoodle.Domain.Collect.Common
open NUnit.Framework

[<TestFixture>]
type TestPublisherInfo () =

    let (Ok validEmail) = EmailAddress.create "Hans.Peter@web.de"

    let validPublisherInfo = 
        {
            Name = "Hans"
            Email = validEmail
        }

    [<Test>]
    member this.TestValidate () =
        validPublisherInfo
        |> PublisherInfo.validate (fun publisherInfo -> true)
        |> fun publisherInfo -> 
            match publisherInfo with
            | Ok info -> Assert.That(info, Is.EqualTo(validPublisherInfo))
            | _ -> Assert.Fail()

    [<Test>]
    member this.TestValidateAlreadySendError () =
        validPublisherInfo
        |> PublisherInfo.validate (fun publisherInfo -> false)
        |> fun publisherInfo ->
            match publisherInfo with
            | Error error -> Assert.That(error, Is.TypeOf<PublisherAlreadySendNamesError>())
            | _ -> Assert.Fail()