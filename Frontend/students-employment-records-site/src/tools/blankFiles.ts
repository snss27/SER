export class BlankFiles {
    constructor(
        public fileUrls: string[],
        public files: File[],
        public maxFiles: number
    ) {}

    public static create(maxFiles: number): BlankFiles {
        return new BlankFiles([], [], maxFiles)
    }

    public withChangedUrls(urls: string[]): BlankFiles {
        return this.modify((bf) => (bf.fileUrls = urls))
    }

    public withChangedFiles(files: File[]): BlankFiles {
        return this.modify((bf) => (bf.files = files))
    }

    private modify(modifier: (blankFiles: BlankFiles) => void): BlankFiles {
        const blankFiles = this.copy()
        modifier(blankFiles)
        return blankFiles
    }

    private copy(): BlankFiles {
        return new BlankFiles([...this.fileUrls], [...this.files], this.maxFiles)
    }
}
