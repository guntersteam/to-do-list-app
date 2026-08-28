import { Service } from '@angular/core';
import { Subject } from 'rxjs';

export interface Toast{
    message: string;
    type: 'success' | 'error'
}

@Service()
export class ToastService {
    public toast$ = new Subject<Toast| null>();

    show(message: string, type: 'success' | 'error' = 'success'){
        this.toast$.next({message,type})
        
        setTimeout(() =>{
            this.clear()
        },3000)
    }

    clear(){
        this.toast$.next(null)
    }

}
