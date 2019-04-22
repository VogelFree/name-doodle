namespace NameDoodle.Domain.Collect.SaveNames

// -----------------------------------------------
// inputs to the Workflow
//
type Female = Female of string
type Male = Male of string
type EmailAddress = EmailAddress of string

type Gender =
  | Female of Female
  | Male of Male

type UnvalidatedPublisherInfo =
  {
    Name : string
    Email : EmailAddress
  }

type UnvalidatedNameInfo =
  {
    Parts: string list
    Gender: Gender
  }

type UnvalidatedName =
  {
    Info : UnvalidatedNameInfo
    Publisher : UnvalidatedPublisherInfo
  }

// -----------------------------------------------
// output from the workflow (success case)
//
type SaveNamesEvent = Undefined

// -----------------------------------------------
// error output
//
type NameAlreadyExistsError = NameAlreadyExistsError of string

type AlreadySendNamesError = AlreadySendNamesError of string

type SaveNamesError =
  | AlreadyExists of NameAlreadyExistsError
  | AlreadySend of AlreadySendNamesError


// -----------------------------------------------
// the workflow itself
//
type SaveNames =
    UnvalidatedName list -> Result<SaveNamesEvent list, SaveNamesError>
