namespace NameDoodle.Domain.Collect.Common

open System.Text.RegularExpressions

type EmailAddress = private EmailAddress of string

type InvalidEmailAddressError = private InvalidEmailAddressError of string

module EmailAddress =
    //  RFC 2822 Standard for email validation
    let private emailCheckRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"

    let create (emailaddress : string) : Result<EmailAddress, InvalidEmailAddressError> =
        if Regex(emailCheckRegex).IsMatch(emailaddress)
        then Ok (EmailAddress emailaddress)
        else Error (InvalidEmailAddressError emailaddress)

    let value (EmailAddress email) = 
        email