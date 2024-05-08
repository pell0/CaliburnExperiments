using Caliburn.Micro;
using CaliburnExperiments.ViewModels.ModalPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaliburnExperiments.ViewModels;

public class ShellViewModel:Conductor<IScreen>
{
    private readonly IEventAggregator _eventAggregator;
    public ShellViewModel(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
       
        if(eventAggregator != null) 
        { 
            _eventAggregator.SubscribeOnUIThread(this);
        }    
        
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);      
    } 

    public async Task Page1()
    {
        await OpenModal(new Page1ViewModel());
    }

    public async Task Page2()
    {
        await OpenModal(new Page2ViewModel());
    }

    public async Task OpenModal(IScreen screen)
    {
       
        await ActivateItemAsync(screen);
        ModalIsVisible = true;
    }

    private bool _modalIsVisible;
    public bool ModalIsVisible { get { return _modalIsVisible; } set { _modalIsVisible = value; NotifyOfPropertyChange(() => ModalIsVisible); } }
    public async Task CloseModal()
    {
        await DeactivateItemAsync(ActiveItem,true, new CancellationToken());        
        ModalIsVisible = false;
    }    
}
