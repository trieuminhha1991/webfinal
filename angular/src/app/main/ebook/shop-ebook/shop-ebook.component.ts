import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-shop-ebook',
  templateUrl: './shop-ebook.component.html',
  styleUrls: ['./shop-ebook.component.css'],
    animations: [appModuleAnimation()]
})
export class ShopEbookComponent extends AppComponentBase {
    constructor(
        injector: Injector
    ) {
        super(injector);
    }
}
