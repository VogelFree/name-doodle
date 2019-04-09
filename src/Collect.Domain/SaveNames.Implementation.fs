module internal NameDoodle.Domain.Collect.SaveNames.Implementation

// -----------------------------------------------
// Validation step
//
type CheckNameInfoExists =
  UnvalidatedNameInfo -> bool

type ValidatedNameInfo = UnvalidatedNameInfo

type ValidateNameInfo =
  CheckNameInfoExists
    -> UnvalidatedNameInfo
    -> Result<ValidatedNameInfo, NameAlreadyExistsError>

type CheckPublisherInfoExists =
  UnvalidatedPublisherInfo -> bool

type ValidatedPublisherInfo = UnvalidatedPublisherInfo

type ValidatePublisherInfo =
  CheckPublisherInfoExists
    -> UnvalidatedPublisherInfo
    -> Result<ValidatedPublisherInfo, AlreadySendNamesError>
