namespace NameDoodle.Domain.Collect.Test

open NameDoodle.Collect.Domain.PublisherInfo
open NameDoodle.Domain.Collect.Common
open NameDoodle.Domain.Collect.SubmitNames
open NUnit.Framework

[<TestFixture>]
type TestSubmitNamesWorkflow () =

    let (Ok validEmail) = EmailAddress.create "Hans.Peter@web.de"
    let (Ok validName) = Name.create "Hans"

    let validPublisherInfo = 
        {
            Name = "Hans"
            Email = validEmail
        }

    let validNames =
        [ 
            {
                Gender = Male 
                Name = validName 
            }
        ]

    let validSubmitNamesData =
        {
            Publisher = validPublisherInfo
            Names = validNames
        }

    let assertNames (names : NameInfo list) (name : NameInfo) =
        Assert.That(names, Has.Some.EqualTo(name))

    let assertErrorResult (result : Result<NamesSubmittedEvent, PublisherAlreadySubmittedError>) =
        match result with
        | Error error -> Assert.That(error, Is.TypeOf<PublisherAlreadySubmittedError>())
        | _ -> Assert.Fail()

    let assertOkResult (result : Result<NamesSubmittedEvent, PublisherAlreadySubmittedError>) =
        match result with
        | Ok submitNamesEvent -> Assert.That(submitNamesEvent, Is.TypeOf<NamesSubmittedEvent>())
        | _ -> Assert.Fail()

    [<Test>]
    member this.TestSubmitNames () =
        validSubmitNamesData
        |> SubmitNamesWorkflow.submitNames 
            (fun nameInfo -> false)
            (assertNames validNames)
            (fun nameInfo -> Assert.Fail())
            (fun publisherInfo -> false)
        |> assertOkResult

    [<Test>]
    member this.TestSubmitNamesNameExists () =
        validSubmitNamesData
        |> SubmitNamesWorkflow.submitNames 
            (fun nameInfo -> true)
            (fun nameInfo -> Assert.Fail())
            (assertNames validNames)
            (fun publisherInfo -> false)
        |> assertOkResult

    [<Test>]
    member this.TestSubmitNamesPublisherAlreadySubmitted () =
        validSubmitNamesData
        |> SubmitNamesWorkflow.submitNames 
            (fun nameInfo -> true)
            (fun nameInfo -> Assert.Fail())
            (fun nameInfo -> Assert.Fail())
            (fun publisherInfo -> true)
        |> assertErrorResult

    [<Test>]
    member this.TestSubmitNamesNameAlreadySubmitted () =
        validSubmitNamesData
        |> SubmitNamesWorkflow.submitNames 
            (fun nameInfo -> true)
            (fun nameInfo -> Assert.Fail())
            (assertNames validNames)
            (fun publisherInfo -> false)
        |> assertOkResult