SELECT 
"crF"."Symbol" as "From",
"crT"."Symbol" as To,
"ex"."Name" as Exch

FROM "Pairs"
inner join "Currencies" "crF"
on "crF"."Id" = "CurrencyFromId"

inner join "Currencies" as "crT"
on "crT"."Id" = "CurrencyToId"

inner join "Exchanges" ex
on "ex"."Id" = "ExchangeId"

where "crF"."Symbol" = 'ETH'
and "crT"."Symbol" = 'BTC'

order by Exch