export const PortfolioTitle = ({ title }: { title: string }) => {
  return (
    <div className="flex items-center gap-2">
      <h1 className="text-3xl font-bold">{title}</h1>
      {/* <Dialog>
        <DialogTrigger asChild>
          <EditButton />
        </DialogTrigger>
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle>Edit profile</DialogTitle>
            <DialogDescription>
              Make changes to your profile here. Click save when you're done.
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="name" className="text-right">
                Title
              </Label>
              <Input
                id="name"
                defaultValue={title}
                className="col-span-3"
                type="text"
              />
            </div>
          </div>
          <DialogFooter>
            <Button type="submit">Save changes</Button>
          </DialogFooter>
        </DialogContent>
      </Dialog> */}
    </div>
  );
};
