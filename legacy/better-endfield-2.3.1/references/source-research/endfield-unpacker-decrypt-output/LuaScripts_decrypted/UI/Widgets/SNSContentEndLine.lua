local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')

SNSContentEndLine = HL.Class('SNSContentEndLine', SNSContentBase)

SNSContentEndLine._OnSNSContentInit = HL.Override() << function(self)
end

HL.Commit(SNSContentEndLine)
return SNSContentEndLine

