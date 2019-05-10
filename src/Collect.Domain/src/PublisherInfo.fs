namespace NameDoodle.Collect.Domain.PublisherInfo

open NameDoodle.Domain.Collect.Common

type PublisherInfo =
    {
        Name : string
        Email : EmailAddress
    }

type CheckPublisherExists =
    PublisherInfo -> bool

type PublisherAlreadySendNamesError = 
    private PublisherAlreadySendNamesError of PublisherInfo

type ValidatePublisherInfo =
    PublisherInfo -> Result<PublisherInfo, PublisherAlreadySendNamesError>

module PublisherInfo =
    let validate 
        (checkPublisherExists : CheckPublisherExists)
        : ValidatePublisherInfo = 
        fun publisherInfo -> 
            if checkPublisherExists publisherInfo
            then Ok(publisherInfo)
            else Error(PublisherAlreadySendNamesError publisherInfo)