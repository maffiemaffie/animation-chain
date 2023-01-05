window.addEventListener('load', initAnimations);

function initAnimations() {
    const queues = document.querySelectorAll('.m-animation-queue');
    const animations = document.querySelectorAll('.m-animation');

    for (let animation of animations) {
        const previous = animation.querySelector('.m-toolbar__previous > button');
        const next = animation.querySelector('.m-toolbar__next > button');

        previous.addEventListener('click',  () => { (swap.bind(animation))(-1, '.m-animation') });
        next.addEventListener('click',      () => { (swap.bind(animation))(1, '.m-animation')  });
    }

    for (let queue of queues) {
        const previous = queue.querySelector('.m-toolbar__previous > button');
        const next = queue.querySelector('.m-toolbar__next > button');

        previous.addEventListener('click',  () => { (swap.bind(queue))(-1, '.m-animation-queue') });
        next.addEventListener('click',      () => { (swap.bind(queue))(1, '.m-animation-queue')  });
    }
}

function swap(direction, type) {
    const elems = Array.from(this.parentNode.querySelectorAll(type));
    const index = elems.findIndex(a => a === this);

    if (index + direction < 0 || index + direction >= elems.length) return;
    if (direction === 1)  elems[index + 1].insertAdjacentElement('afterend', this);
    if (direction === -1) elems[index - 1].insertAdjacentElement('beforebegin', this);
}