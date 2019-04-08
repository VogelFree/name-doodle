module internal NameDoodle.Domain.Collect.SaveNames.Implementation

// -----------------------------------------------
// Validation step
//
type CheckNameInfoExists =
  UnvalidatedNameInfo -> bool

type CheckPublisherInfoExists =
  UnvalidatedPublisherInfo -> bool
