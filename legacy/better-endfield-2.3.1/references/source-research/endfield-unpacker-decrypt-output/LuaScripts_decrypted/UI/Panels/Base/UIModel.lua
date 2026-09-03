UIModel = HL.Class("UIModel")


UIModel.InitModel = HL.Virtual() << function(self)
end


UIModel.OnClose = HL.Virtual() << function(self)
end

HL.Commit(UIModel)
