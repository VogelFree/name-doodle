namespace NameDoodle.Domain.Collect.SubmitNames

open NameDoodle.Domain.Collect.Common
open NameDoodle.Collect.Domain.PublisherInfo

// -----------------------------------------------
// inputs to the Workflow
//

type Gender =
  | Female
  | Male

type NameInfo =
  {
    Name: Name
    Gender: Gender
  }

type SubmitNamesData =
  {
    Publisher : PublisherInfo
    Names : NameInfo list
  }

// -----------------------------------------------
// output from the workflow (success case)
//
type NamesSubmittedEvent = NamesSubmittedEvent of PublisherInfo

// -----------------------------------------------
// error output
//
type PublisherAlreadySubmittedError = PublisherAlreadySubmittedError of PublisherInfo

// -----------------------------------------------
// the workflow itself
//
type SubmitNames =
    SubmitNamesData -> Result<NamesSubmittedEvent, PublisherAlreadySubmittedError>

// -----------------------------------------------
// Dependencies
//
type ExistsName =
  NameInfo -> bool

type SaveName =
  NameInfo -> Unit

type VoteName =
  NameInfo -> Unit

module SubmitNamesWorkflow =

  let saveOrVoteName 
    (saveName : SaveName)
    (voteName : VoteName)
    (existsName : ExistsName) =
    fun (name : NameInfo) ->
      if existsName name
      then voteName name
      else saveName name

  let submitNames
    (existsName : ExistsName)
    (saveName : SaveName)
    (voteName : VoteName)
    (checkPublisher : CheckPublisherExists)
    : SubmitNames =
    fun submitNamesData -> 
      if not (checkPublisher submitNamesData.Publisher)
      then 
        submitNamesData.Names
        |> List.iter (saveOrVoteName saveName voteName existsName)
        Ok (NamesSubmittedEvent submitNamesData.Publisher)
      else Error (PublisherAlreadySubmittedError submitNamesData.Publisher)