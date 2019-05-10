namespace NameDoodle.Domain.Collect.Common

open System.Text.RegularExpressions

type Name = private Name of string

type InvalidNameFormatError = private InvalidNameFormatError of string

module Name =
    let nameCheckRegex = @"[^a-zA-Z\-]"
    let create (name : string) : Result<Name, InvalidNameFormatError> =
        if not (Regex(nameCheckRegex).IsMatch(name))
        then Ok (Name name)
        else Error (InvalidNameFormatError "Only a-z, A-Z and - are allowed")
        
    let value (Name name) = 
        name

