import {ComponentFixture} from '@angular/core/testing';
import {By} from '@angular/platform-browser';
import {DebugElement} from '@angular/core';

export class DOMHelper<Type> {
  private fixture: ComponentFixture<Type>;

  constructor(fixture: ComponentFixture<Type>) {
    this.fixture = fixture;
  }

  singleTextFromElement(tagName: string): string {
    const elementDes = this.fixture.debugElement.query(By.css(tagName));
    if (elementDes) {
      return elementDes.nativeElement.textContent;
    }
  }

  getAllOfElementsByTag(tagName: string): DebugElement[] {
    const elementDes = this.fixture.debugElement.queryAll(By.css(tagName));
    if (elementDes) {
      return elementDes;
    }
  }

  getWithElementsContainingText(tagName: string, containerText: string): DebugElement[] {
    const elements = this.fixture.debugElement.queryAll(By.css(tagName));
    return elements.filter( element => element.nativeElement.textContent === containerText);
  }

  clickItemsWithName(itemToClick: string, buttonContent: string) {
    this.getWithElementsContainingText(itemToClick, buttonContent).forEach(btn => {
      console.log('button: ' + buttonContent + ' was clicked');
      btn.nativeElement.click();
      this.fixture.detectChanges();
    });

  }

  fillAllInputsAndSelects(){
    this.getAllOfElementsByTag('input').forEach( element => {
      element.nativeElement.textContent = 'Testing';
    });
    this.getAllOfElementsByTag('textarea').forEach( element => {
      element.nativeElement.textContent = 'Testing';
    });
  }
}
